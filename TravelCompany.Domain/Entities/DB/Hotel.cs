using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelCompany.Domain.Entities.DB
{
    [Table("Hotel")]
    public class Hotel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Stars { get; set; }
        public string City { get; set; } = string.Empty;
        public bool Available { get; set; }

        public virtual ICollection<Room>? Rooms { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
    }
}
