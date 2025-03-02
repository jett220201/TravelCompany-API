using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TravelCompany.Application.Service;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Application.Helper;
using TravelCompany.Application.Contracts;
using System.Globalization;
using System.Text;
using TravelCompany.Domain.Entities.Enum;

namespace TravelCompanyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly HotelService _hotelService;
        private readonly RoomService _roomService;
        private readonly BookingService _bookingService;
        private readonly BookingGuestService _bookingGuestService;
        private readonly BookingRoomService _bookingRoomService;
        private readonly GuestService _guestService;
        public CustomerController(HotelService hotelService, RoomService roomService, BookingService bookingService,
                                BookingRoomService bookingRoomService, BookingGuestService bookingGuestService, GuestService guestService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _bookingService = bookingService;
            _bookingGuestService = bookingGuestService;
            _bookingRoomService = bookingRoomService;
            _guestService = guestService;
        }
        
        [HttpGet("search")]
        [SwaggerOperation(Summary = "Search for hotels", Description = "Search for hotels based on check-in and check-out dates, number of guests, and city.")]
        [SwaggerResponse(200, "Returns a list of hotels", typeof(List<Hotel>))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> SearchHotel(string checkIn, string checkOut, int guest, string city)
        {
            try
            {
                if(guest == 0) return BadRequest("Guests must be greater than 0.");
                List<string> cities = await _hotelService.GetAllCities();
                if (!cities.Contains(city)) return BadRequest("We do not have hotels in the city you are looking for.");
                DateOnly checkInDate = DateOnly.Parse(checkIn, CultureInfo.InvariantCulture);
                DateOnly checkOutDate = DateOnly.Parse(checkOut, CultureInfo.InvariantCulture);
                if(checkInDate < DateOnly.FromDateTime(DateTime.Now)) return BadRequest("CheckIn date must be after today.");
                else if(checkOutDate < checkInDate) return BadRequest("CheckOut date must be after CheckIn date.");
                List<Hotel> hotels = await _hotelService.SearchHotels(checkInDate, checkOutDate, guest, city);
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message,
                });
            }
        }

        [HttpPost("booking/new")]
        [SwaggerOperation(Summary = "Create a new booking", Description = $"Create a new booking with the provided booking request details.\t\n" +
            $"Enums help: \t\n" +
            $"Gender: \t\n" +
            $"0 = {nameof(Gender.Male)}\t\n" +
            $"1 = {nameof(Gender.Female)}\t\n" +
            $"2 = {nameof(Gender.Other)}\t\n" +
            $"IdentityType: \t\n" +
            $"0 = {nameof(IdentityType.DNI)}\t\n" +
            $"1 = {nameof(IdentityType.CC)}\t\n" +
            $"2 = {nameof(IdentityType.Passport)}\t\n")]
        [SwaggerResponse(200, "Booking created successfully", typeof(Booking))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> MakeBooking([FromBody] BookingRequest bookingRequest)
        {
            try
            {
                if (bookingRequest.CheckIn >= bookingRequest.CheckOut) return BadRequest("CheckIn date must be after CheckOut date.");

                if (string.IsNullOrEmpty(bookingRequest.EmergencyContactName) || string.IsNullOrEmpty(bookingRequest.EmergencyContactPhone))
                    return BadRequest("The emergency contact information is mandatory.");

                List<Room> rooms = await _roomService.GetRoomsListById(bookingRequest.RoomIds);
                if (rooms.Count != bookingRequest.RoomIds.Count) throw new InvalidDataException($"The room's Id {string.Join(", ", bookingRequest.RoomIds.Where(x => rooms.Select(r => r.Id).Contains(x)))} has not been found");

                if (rooms.TrueForAll(x => !x.Available)) return BadRequest("None of the selected rooms are available.");

                bool isHotelAvailable = await _bookingService.IsHotelAvailable(bookingRequest.CheckIn, bookingRequest.CheckOut, rooms.FirstOrDefault().HotelId);
                if (!isHotelAvailable) return BadRequest("The select hotel is not available for the selected dates.");

                Booking newBooking = await _bookingService.AddReturn(new Booking
                {
                    CheckIn = bookingRequest.CheckIn,
                    CheckOut = bookingRequest.CheckOut,
                    Guests = bookingRequest.Guests.Count,
                    HotelId = rooms.FirstOrDefault().HotelId,
                    EmergencyContactName = bookingRequest.EmergencyContactName,
                    EmergencyContactPhone = bookingRequest.EmergencyContactPhone,
                });

                await _bookingRoomService.AddRange(rooms.Select(r => new BookingRoom
                {
                    BookingId = newBooking.Id,
                    RoomId = r.Id
                }).ToList());

                await _guestService.AddRange(bookingRequest.Guests);

                await _bookingGuestService.AddRange(bookingRequest.Guests.Select(g => new BookingGuest
                {
                    BookingId = newBooking.Id,
                    GuestId = g.Id
                }).ToList());

                var stringBuilder = new StringBuilder();
                foreach (var guest in bookingRequest.Guests)
                {
                    stringBuilder.Append($"Dear {guest.Name}.\n");
                    stringBuilder.Append($"Your reservation from {bookingRequest.CheckIn} to {bookingRequest.CheckOut}\n");
                    stringBuilder.Append($"for {bookingRequest.Guests.Count} persons has been successfully processed.\n");
                    stringBuilder.Append("We are so happy to have you stay with us!");
                    await NotifyCustomer(guest, stringBuilder.ToString());
                }

                return Ok(new
                {
                    status = "ok",
                    message = "Booking done.",
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    status = "error",
                    message = ex.Message,
                });
            }
        }

        private async Task<JsonResult> NotifyCustomer(Guest guest, string content)
        {
            try
            {
                await EmaiSenderHelper.SendEmail(guest.Email, content);
                return Json(new
                {
                    status = "ok",
                    message = "Email sent",
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = "error",
                    message = ex.Message,
                });
            }
        }
    }
}
