using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Infraestructure.Persistence;

namespace TravelCompany.Infraestructure.Repository
{
    public class BookingRoomRepository : GenericRepository<BookingRoom>, IBookingRoomRepository
    {
        private readonly IDbContextFactory<CoreDBContext> _contextFactory;
        public BookingRoomRepository(IDbContextFactory<CoreDBContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
