using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.Domain.Entities.DB
{
    [Table("Booking")]
    public class Booking
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public DateOnly CheckIn { get; set; }
        public DateOnly CheckOut { get; set; }
        public int Guests {  get; set; }
        public int HotelId { get; set; }
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;

        public virtual Hotel? Hotel { get; set; }
        public virtual ICollection<BookingGuest>? BookingGuest { get; set; }
        public virtual ICollection<BookingRoom>? BookingRooms { get; set; }
    }
}
