using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Infraestructure.Persistence;

namespace TravelCompany.Infraestructure.Repository
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        private readonly IDbContextFactory<CoreDBContext> _contextFactory;
        public BookingRepository(IDbContextFactory<CoreDBContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            var context = await _contextFactory.CreateDbContextAsync();
            return await context.Booking
                .Include(x => x.Hotel)
                .Include(x => x.BookingGuest)
                .ThenInclude(x => x.Guest)
                .Include(x => x.BookingRooms)
                .ThenInclude(x => x.Room)
                .ToListAsync();
        }

        public async Task<Booking> GetFullBookingById(int id)
        {
            var context = await _contextFactory.CreateDbContextAsync();
            return await context.Booking
                .Include(x => x.Hotel)
                .Include(x => x.BookingGuest)
                .ThenInclude(x => x.Guest)
                .Include(x => x.BookingRooms)
                .ThenInclude(x => x.Room)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> IsHotelAvailable(DateOnly checkIn, DateOnly checkOut, int hotelId)
        {
            var context = await _contextFactory.CreateDbContextAsync();
            return ! await context.Booking.AnyAsync(x => x.HotelId == hotelId &&
                ((checkIn >= x.CheckIn && checkIn < x.CheckOut) ||
                (checkOut > x.CheckIn && checkOut <= x.CheckOut) ||
                (checkIn <= x.CheckIn && checkOut >= x.CheckOut)));
        }
    }
}
