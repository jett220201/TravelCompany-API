using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Service
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetAllRooms()
        {
            return (List<Room>)await _roomRepository.GetAll();
        }

        public async Task<int> CreateRoom(Room room)
        {
            return await _roomRepository.Create(room);
        }

        public async Task<Room> GetById(int Id)
        {
            return await _roomRepository.GetById(Id);
        }

        public async Task<Room> Update(Room room)
        {
            return await _roomRepository.UpdateReturn(room);
        }

        public async Task<List<Room>> GetRoomsListById(List<int> ids)
        {
            return await _roomRepository.GetAllRooms(ids);
        }
    }
}
