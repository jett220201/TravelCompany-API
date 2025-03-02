using TravelCompanyAPI.Extensions;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { 
        Title = "TravelCompanyAPI", 
        Version = "v1", 
        Description = "API to manage a travel agency. In it you can interact as a customer with hotel search and booking creation. As an administrator you can search for hotels, rooms and reservations, as well as create and edit them."
    });
    c.EnableAnnotations();
});

builder.Services.RegisterServices();
builder.Services.RegisterDataSource(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
