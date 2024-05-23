using FoodDelivery.RestaurantCatalogApi.Infrastructure;
using FoodDelivery.RestaurantCatalogApi.Infrastructure.Extention;
using FoodDelivery.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.AddDefaultOpenApi();
builder.AddDefaultAuthentication();
builder.AddAplicationServices();
builder.AddSerilog();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();

var app = builder.Build();
app.UseDefaultOpenApi();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RestaurantCatalogContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
