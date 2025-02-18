using TravelCompany.Application.Interface;
using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Service
{
    public class GuestService
    {
        private readonly IGuestRepository _guestRepository;
        public GuestService(IGuestRepository guestRepository)
        {
            _guestRepository = guestRepository;
        }

        public async Task<int> Create(Guest guest)
        {
            return await _guestRepository.Create(guest);
        }

        public async Task AddRange(List<Guest> guest)
        {
            await _guestRepository.AddRange(guest);
        }
    }
}
