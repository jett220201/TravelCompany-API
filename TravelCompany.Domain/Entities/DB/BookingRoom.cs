using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.Domain.Entities.DB
{
    [Table("BookingRoom")]
    public class BookingRoom
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int RoomId { get; set; }

        public virtual Booking? Booking { get; set; }
        public virtual Room? Room { get; set; }
    }
}
