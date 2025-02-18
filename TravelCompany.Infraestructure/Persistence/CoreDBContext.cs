using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TravelCompany.Domain.Entities.DB;
using TravelCompany.Domain.Entities.Enum;

namespace TravelCompany.Infraestructure.Persistence
{
    public class CoreDBContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        public CoreDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingGuest> BookingGuest { get; set; }
        public DbSet<BookingRoom> BookingRoom { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<Room> Room { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? _configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(connectionString))
            {
                optionsBuilder.UseSqlServer(connectionString);   
            }
            else
            {
                throw new ArgumentNullException("The connection string in empty");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CheckIn).HasColumnName("CheckIn");
                entity.Property(e => e.CheckOut).HasColumnName("CheckOut");
                entity.Property(e => e.Guests).HasColumnName("Guests");
                entity.Property(e => e.HotelId).HasColumnName("HotelId");
                entity.Property(e => e.EmergencyContactName).HasColumnName("EmergencyContactName");
                entity.Property(e => e.EmergencyContactPhone).HasColumnName("EmergencyContactPhone");

                entity.HasOne(e => e.Hotel)
                      .WithMany(h => h.Bookings)
                      .HasForeignKey(e => e.HotelId);
            });

            modelBuilder.Entity<BookingGuest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BookingId).HasColumnName("BookingId");
                entity.Property(e => e.GuestId).HasColumnName("GuestId");

                entity.HasOne(e => e.Booking)
                      .WithMany(b => b.BookingGuest)
                      .HasForeignKey(bg => bg.BookingId);

                entity.HasOne(e => e.Guest)
                      .WithMany(g => g.BookingGuest)
                      .HasForeignKey(bg => bg.GuestId);
            });

            modelBuilder.Entity<BookingRoom>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BookingId).HasColumnName("BookingId");
                entity.Property(e => e.RoomId).HasColumnName("RoomId");

                entity.HasOne(e => e.Booking)
                      .WithMany(b => b.BookingRooms)
                      .HasForeignKey(br => br.BookingId);

                entity.HasOne(e => e.Room)
                      .WithMany(b => b.BookingRoom)
                      .HasForeignKey(br => br.RoomId);
            });

            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.LastName).HasColumnName("LastName");
                entity.Property(e => e.BirthDate).HasColumnName("BirthDate");
                entity.Property(e => e.Gender).HasConversion(v => v.ToString().ToUpper(), v => (Gender)Enum.Parse(typeof(Gender), v, true)).HasColumnName("Gender");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Phone).HasColumnName("Phone");
                entity.Property(e => e.IdentityType).HasConversion<string>().HasColumnName("IdentityType");
                entity.Property(e => e.IdentityNumber).HasColumnName("IdentityNumber");
            });

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Stars).HasColumnName("Stars");
                entity.Property(e => e.City).HasColumnName("City");
                entity.Property(e => e.Available).HasColumnName("Available");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HotelId).HasColumnName("HotelId");
                entity.Property(e => e.RoomTypeId).HasConversion<int>().HasColumnName("RoomTypeId");
                entity.Property(e => e.Capacity).HasColumnName("Capacity");
                entity.Property(e => e.Rate).HasColumnName("Rate");
                entity.Property(e => e.Tax).HasColumnName("Tax");
                entity.Property(e => e.CurrencyCode).HasConversion<string>().HasColumnName("CurrencyCode");
                entity.Property(e => e.Floor).HasColumnName("Floor");
                entity.Property(e => e.Available).HasColumnName("Available");

                entity.HasOne(e => e.Hotel)
                      .WithMany(e => e.Rooms)
                      .HasForeignKey(e => e.HotelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
