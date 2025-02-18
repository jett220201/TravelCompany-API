using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Interface
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<List<Booking>> GetAllBookings();
        Task<Booking> GetFullBookingById(int id);
        Task<bool> IsHotelAvailable(DateOnly checkIn, DateOnly checkOut, int hotelId);
    }
}
