using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Infraestructure.Persistence;

namespace TravelCompany.Infraestructure.Repository
{
    public class BookingGuestRepository : GenericRepository<BookingGuest>, IBookingGuestRepository
    {
        private readonly IDbContextFactory<CoreDBContext> _contextFactory;
        public BookingGuestRepository(IDbContextFactory<CoreDBContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
