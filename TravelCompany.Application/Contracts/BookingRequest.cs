using TravelCompany.Domain.Entities.DB;

namespace TravelCompany.Application.Contracts
{
    public class BookingRequest
    {
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public List<int> RoomIds { get; set; } = new List<int>();
        public List<Guest> Guests { get; set; } = new List<Guest>();
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
    }
}
