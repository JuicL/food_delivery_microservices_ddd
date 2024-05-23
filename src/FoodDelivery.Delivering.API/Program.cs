using FoodDelivery.Delivering.API.Application.Services.SignalR;
using FoodDelivery.Delivering.Infrastructure;
using FoodDelivery.DeliveringAPI.Extentions;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
builder.AddDefaultOpenApi();
builder.AddDefaultAuthentication();
builder.AddSerilog();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();


var app = builder.Build();
app.UseDefaultOpenApi();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DeliveryContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<DeliveryHub>("/hubs/deliveryHub");
app.MapControllers();

app.Run();
