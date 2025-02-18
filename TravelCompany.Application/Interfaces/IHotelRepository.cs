using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Interface
{
    public interface IHotelRepository : IGenericRepository<Hotel>
    {
        Task<List<Hotel>> SearchHotels(DateOnly checkIn, DateOnly checkOut, int guests, string city);
    }
}
