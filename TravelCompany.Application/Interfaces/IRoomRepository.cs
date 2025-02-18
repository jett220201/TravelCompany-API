using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Interface
{
    public interface IRoomRepository : IGenericRepository<Room>
    {
        Task<List<Room>> GetAllRooms(List<int> ids);
    }
}
