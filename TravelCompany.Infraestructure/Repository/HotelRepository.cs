using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Infraestructure.Persistence;

namespace TravelCompany.Infraestructure.Repository
{
    public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
    {
        private readonly IDbContextFactory<CoreDBContext> _contextFactory;
        public HotelRepository(IDbContextFactory<CoreDBContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            var context = await _contextFactory.CreateDbContextAsync();
            var hotels = await context.Hotel
                .Include(x => x.Rooms)
                .Include(x => x.Bookings)
                .ToListAsync();
            return hotels;
        }

        public async Task<List<Hotel>> SearchHotels(DateOnly checkIn, DateOnly checkOut, int guests, string city)
        {
            var context = await _contextFactory.CreateDbContextAsync();
            var hotels = await context.Hotel
                .Include(x => x.Bookings)
                .Include(x => x.Rooms.Where(r => r.Capacity >= guests))
                .Where(x => x.City.ToLower().Contains(city.ToLower())
                            && x.Available
                            && !x.Bookings.Any(b => 
                            (checkIn >= b.CheckIn && checkIn < b.CheckOut) ||
                            (checkOut > b.CheckIn && checkOut <= b.CheckOut) ||
                            (checkIn <= b.CheckIn && checkOut >= b.CheckOut))
                            && x.Rooms.Sum(x => x.Capacity) >= guests)
                .ToListAsync();
            return hotels;
        }
    }
}
