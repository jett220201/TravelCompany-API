using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Service
{
    public class BookingGuestService
    {
        private readonly IBookingGuestRepository _bookingGuestRepository;
        public BookingGuestService(IBookingGuestRepository bookingGuestRepository)
        {
            _bookingGuestRepository = bookingGuestRepository;
        }
        public async Task AddRange(List<BookingGuest> bookingGuests)
        {
            await _bookingGuestRepository.AddRange(bookingGuests);
        }
    }
}
