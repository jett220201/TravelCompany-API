using TravelCompany.Application.Interface;
using TravelCompany.Application.Service;
using TravelCompany.Infraestructure.Repository;
using TravelCompany.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TravelCompanyAPI.Extensions
{
    public static class AppExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingGuestRepository, BookingGuestRepository>();
            services.AddScoped<IBookingRoomRepository, BookingRoomRepository>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            // Services
            services.AddScoped<BookingService>();
            services.AddScoped<BookingGuestService>();
            services.AddScoped<BookingRoomService>();
            services.AddScoped<GuestService>();
            services.AddScoped<HotelService>();
            services.AddScoped<RoomService>();
        }

        public static void RegisterDataSource(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextFactory<CoreDBContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("travelConnection")));
        }
    }
}
