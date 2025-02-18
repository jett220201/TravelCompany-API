using Microsoft.AspNetCore.Mvc;
using TravelCompany.Application.Service;
using TravelCompany.Domain.Entities.DB;

namespace TravelCompanyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly HotelService _hotelService;
        private readonly RoomService _roomService;
        private readonly BookingService _bookingService;

        public AdminController(HotelService hotelService, RoomService roomService, BookingService bookingService)
        {
            _roomService = roomService;
            _bookingService = bookingService;
            _hotelService = hotelService;
        }

        [HttpGet("hotel/all")]
        public async Task<JsonResult> GetAllHotels()
        {
            try
            {
                List<Hotel> hotels = await _hotelService.GetAllHotels();
                return Json(hotels);
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

        [HttpGet("booking/all")]
        public async Task<JsonResult> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookings();
                return Json(bookings);
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

        [HttpGet("booking/{id}")]
        public async Task<JsonResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingService.GetFullBookingById(id);
                return Json(booking);
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

        [HttpPost("hotel/create")]
        public async Task<JsonResult> CreateHotel(Hotel hotel)
        {
            try
            {
                await _hotelService.CreateHotel(hotel);
                return Json(hotel);
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

        [HttpPost("hotel/edit")]
        public async Task<JsonResult> EditHotel(Hotel hotel)
        {
            try
            {
                await _hotelService.Update(hotel);
                return Json(hotel);
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

        [HttpGet("room/all")]
        public async Task<JsonResult> GetAllRooms()
        {
            try
            {
                List<Room> rooms = await _roomService.GetAllRooms();
                return Json(rooms);
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

        [HttpPost("room/create")]
        public async Task<JsonResult> CreateRoom(Room room)
        {
            try
            {
                await _roomService.CreateRoom(room);
                return Json(room);
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

        [HttpPost("room/edit")]
        public async Task<JsonResult> EditRoom(Room room)
        {
            try
            {
                room.Hotel = await _hotelService.GetById(room.HotelId);
                await _roomService.Update(room);
                return Json(room);
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

        [HttpPost("hotel/addRoom")]
        public async Task<JsonResult> AddRoomsToHotel(List<Room> rooms, int hotelId)
        {
            try
            {
                Hotel hotel = await _hotelService.GetById(hotelId);
                hotel.Rooms = rooms;
                Hotel updatedHotel = await _hotelService.Update(hotel);
                return Json(updatedHotel);
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

        [HttpPost("hotel/enable")]
        public async Task<JsonResult> EnableHotel(int id)
        {
            try
            {
                Hotel hotel = await _hotelService.GetById(id);
                if (!hotel.Available)
                {
                    hotel.Available = true;
                    await _hotelService.Update(hotel);
                }
                return Json(hotel);
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

        [HttpPost("hotel/disable")]
        public async Task<JsonResult> DisableHotel(int id)
        {
            try
            {
                Hotel hotel = await _hotelService.GetById(id);
                if (hotel.Available)
                {
                    hotel.Available = false;
                    await _hotelService.Update(hotel);
                }
                return Json(hotel);
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

        [HttpPost("room/enable")]
        public async Task<JsonResult> EnableRoom(int id)
        {
            try
            {
                Room room = await _roomService.GetById(id);
                if (!room.Available)
                {
                    room.Available = true;
                    await _roomService.Update(room);
                }
                return Json(room);
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
        
        [HttpPost("room/disable")]
        public async Task<JsonResult> DisableRoom(int id)
        {
            try
            {
                Room room = await _roomService.GetById(id);
                if (room.Available)
                {
                    room.Available = false;
                    await _roomService.Update(room);
                }
                return Json(room);
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
