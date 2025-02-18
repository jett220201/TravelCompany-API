using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelCompany.Domain.Entities.Enum;

namespace TravelCompany.Domain.Entities.DB
{
    [Table("Guest")]
    public class Guest
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public IdentityType IdentityType { get; set; }
        public string IdentityNumber { get; set; } = string.Empty;

        public virtual ICollection<BookingGuest>? BookingGuest { get; set; }
    }
}
