using FoodDelivery.OrderApi.Extention;
using FoodDelivery.OrderApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.AddDefaultOpenApi();
builder.AddDefaultAuthentication();
builder.AddSerilog();
builder.AddAplicationServices();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();

var app = builder.Build();

app.UseDefaultOpenApi();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OrderingContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
