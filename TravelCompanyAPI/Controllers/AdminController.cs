using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
        [SwaggerOperation(Summary = "Get all hotels", Description = "Retrieve a list of all hotels.")]
        [SwaggerResponse(200, "Returns a list of hotels", typeof(List<Hotel>))]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> GetAllHotels()
        {
            try
            {
                List<Hotel> hotels = await _hotelService.GetAllHotels();
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

        [HttpGet("booking/all")]
        [SwaggerOperation(Summary = "Get all bookings", Description = "Retrieve a list of all bookings.")]
        [SwaggerResponse(200, "Returns a list of bookings", typeof(List<Booking>))]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var bookings = await _bookingService.GetAllBookings();
                return Ok(bookings);
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

        [HttpGet("booking/{id}")]
        [SwaggerOperation(Summary = "Get booking by ID", Description = "Retrieve a booking by its ID.")]
        [SwaggerResponse(200, "Returns the booking", typeof(Booking))]
        [SwaggerResponse(404, "Booking not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            try
            {
                var booking = await _bookingService.GetFullBookingById(id);
                if(booking == null) return NotFound("Booking not found");
                return Ok(booking);
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

        [HttpPost("hotel/create")]
        [SwaggerOperation(Summary = "Create a new hotel", Description = "Create a new hotel with the provided details.")]
        [SwaggerResponse(200, "Hotel created successfully", typeof(Hotel))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                await _hotelService.CreateHotel(hotel);
                return Ok(hotel);
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

        [HttpPost("hotel/edit")]
        [SwaggerOperation(Summary = "Edit an existing hotel", Description = "Edit the details of an existing hotel.")]
        [SwaggerResponse(200, "Hotel edited successfully", typeof(Hotel))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> EditHotel([FromBody] Hotel hotel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _hotelService.Update(hotel);
                return Ok(hotel);
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

        [HttpGet("room/all")]
        [SwaggerOperation(Summary = "Get all rooms", Description = "Retrieve a list of all rooms.")]
        [SwaggerResponse(200, "Returns a list of rooms", typeof(List<Room>))]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> GetAllRooms()
        {
            try
            {
                List<Room> rooms = await _roomService.GetAllRooms();
                return Ok(rooms);
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

        [HttpPost("room/create")]
        [SwaggerOperation(Summary = "Create a new room", Description = "Create a new room with the provided details.")]
        [SwaggerResponse(200, "Room created successfully", typeof(Room))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> CreateRoom(Room room)
        {
            try
            {
                if(!ModelState.IsValid) return BadRequest(ModelState);
                await _roomService.CreateRoom(room);
                return Ok(room);
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

        [HttpPost("room/edit")]
        [SwaggerOperation(Summary = "Edit an existing room", Description = "Edit the details of an existing room.")]
        [SwaggerResponse(200, "Room edited successfully", typeof(Room))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> EditRoom(Room room)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                room.Hotel = await _hotelService.GetById(room.HotelId);
                await _roomService.Update(room);
                return Ok(room);
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

        [HttpPost("hotel/addRoom")]
        [SwaggerOperation(Summary = "Add rooms to a hotel", Description = "Add a list of rooms to a specific hotel.")]
        [SwaggerResponse(200, "Rooms added to hotel successfully", typeof(Hotel))]
        [SwaggerResponse(400, "Invalid input parameters")]
        [SwaggerResponse(404, "Hotel not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> AddRoomsToHotel(List<Room> rooms, int hotelId)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                Hotel hotel = await _hotelService.GetById(hotelId);
                if (hotel == null) return NotFound("Hotel not found");
                hotel.Rooms = rooms;
                Hotel updatedHotel = await _hotelService.Update(hotel);
                return Ok(updatedHotel);
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

        [HttpPost("hotel/enable")]
        [SwaggerOperation(Summary = "Enable a hotel", Description = "Enable a hotel by its ID.")]
        [SwaggerResponse(200, "Hotel enabled successfully", typeof(Hotel))]
        [SwaggerResponse(404, "Hotel not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> EnableHotel(int id)
        {
            try
            {
                Hotel hotel = await _hotelService.GetById(id);
                if(hotel == null) return NotFound("Hotel not found");
                if (!hotel.Available)
                {
                    hotel.Available = true;
                    await _hotelService.Update(hotel);
                }
                return Ok(hotel);
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

        [HttpPost("hotel/disable")]
        [SwaggerOperation(Summary = "Disable a hotel", Description = "Disable a hotel by its ID.")]
        [SwaggerResponse(200, "Hotel disabled successfully", typeof(Hotel))]
        [SwaggerResponse(404, "Hotel not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> DisableHotel(int id)
        {
            try
            {
                Hotel hotel = await _hotelService.GetById(id);
                if(hotel == null) return NotFound("Hotel not found");
                if (hotel.Available)
                {
                    hotel.Available = false;
                    await _hotelService.Update(hotel);
                }
                return Ok(hotel);
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

        [HttpPost("room/enable")]
        [SwaggerOperation(Summary = "Enable a room", Description = "Enable a room by its ID.")]
        [SwaggerResponse(200, "Room enabled successfully", typeof(Room))]
        [SwaggerResponse(404, "Room not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> EnableRoom(int id)
        {
            try
            {
                Room room = await _roomService.GetById(id);
                if (room == null) return NotFound("Room not found");
                if (!room.Available)
                {
                    room.Available = true;
                    await _roomService.Update(room);
                }
                return Ok(room);
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
        
        [HttpPost("room/disable")]
        [SwaggerOperation(Summary = "Disable a room", Description = "Disable a room by its ID.")]
        [SwaggerResponse(200, "Room disabled successfully", typeof(Room))]
        [SwaggerResponse(404, "Room not found")]
        [SwaggerResponse(500, "Internal server error")]
        public async Task<IActionResult> DisableRoom(int id)
        {
            try
            {
                Room room = await _roomService.GetById(id);
                if(room == null) return NotFound("Room not found");
                if (room.Available)
                {
                    room.Available = false;
                    await _roomService.Update(room);
                }
                return Ok(room);
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
    }
}
