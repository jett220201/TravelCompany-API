using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TravelCompany.Domain.Entities.Enum;

namespace TravelCompany.Domain.Entities.DB
{
    [Table("Room")]
    public class Room
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public RoomType RoomTypeId { get; set; }
        public int Capacity { get; set; }
        public decimal Rate { get; set; }
        public decimal Tax { get; set; }
        public Currency CurrencyCode { get; set; }
        public int Floor { get; set; }
        public bool Available { get; set; }

        [JsonIgnore]
        public virtual Hotel? Hotel { get; set; }
        [JsonIgnore]
        public virtual ICollection<BookingRoom>? BookingRoom { get; set; }
    }
}
