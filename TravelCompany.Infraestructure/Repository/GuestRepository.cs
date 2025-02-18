using Microsoft.EntityFrameworkCore;
using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Infraestructure.Persistence;

namespace TravelCompany.Infraestructure.Repository
{
    public class GuestRepository : GenericRepository<Guest>, IGuestRepository
    {
        private readonly IDbContextFactory<CoreDBContext> _contextFactory;
        public GuestRepository(IDbContextFactory<CoreDBContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }
    }
}
