using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Infraestructure.Persistence;

namespace TravelCompany.Infraestructure.Repository
{
    public class RoomRepository : GenericRepository<Room>, IRoomRepository
    {
        private readonly IDbContextFactory<CoreDBContext> _contextFactory;
        public RoomRepository(IDbContextFactory<CoreDBContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<Room> GetById(int id)
        {
            var context = await _contextFactory.CreateDbContextAsync();
            return await context.Room.Include(x => x.Hotel).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Room>> GetAllGeneralRooms()
        {
            var context = await _contextFactory.CreateDbContextAsync();
            return await context.Room.Include(x => x.Hotel).Include(x => x.BookingRoom).ToListAsync();
        }

        public async Task<List<Room>> GetAllRooms(List<int> ids)
        {
            var context = await _contextFactory.CreateDbContextAsync();
            return await context.Room.Include(x => x.Hotel).Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}
