using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Service
{
    public class BookingService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await _bookingRepository.GetAllBookings();
        }

        public async Task<Booking> GetFullBookingById(int id)
        {
            return await _bookingRepository.GetFullBookingById(id);
        }

        public async Task<bool> IsHotelAvailable(DateOnly checkIn, DateOnly checkOut, int hotelId)
        {
            return await _bookingRepository.IsHotelAvailable(checkIn, checkOut, hotelId);
        }


        public async Task<Booking> AddReturn(Booking booking)
        {
            return await _bookingRepository.AddReturn(booking);
        }
    }
}
