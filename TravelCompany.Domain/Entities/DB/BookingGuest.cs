using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.Domain.Entities.DB
{
    [Table("BookingGuest")]
    public class BookingGuest
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int GuestId { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Guest? Guest { get; set; }
    }
}
