using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Service
{
    public class BookingRoomService
    {
        private readonly IBookingRoomRepository _bookingRoomRepository;
        public BookingRoomService(IBookingRoomRepository bookingRoomRepository)
        {
            _bookingRoomRepository = bookingRoomRepository;
        }
        public async Task AddRange(List<BookingRoom> bookingRoom)
        {
            await _bookingRoomRepository.AddRange(bookingRoom);
        }
    }
}
