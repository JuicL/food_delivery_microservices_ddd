using FoodDelivery.IntegrationEventLogEF.Services;
using FoodDelivery.Delivering.API.Application.Services;
using FoodDelivery.Delivering.API.Application.Services.SignalR;
using FoodDelivery.Delivering.Application.Behaviors;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.ÑouriersAgregate;
using FoodDelivery.Delivering.Infrastructure;
using FoodDelivery.Delivering.Infrastructure.Repositories.Implementation;
using FoodDelivery.DeliveringAPI.Extentions;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.ServiceDefaults;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
builder.AddDefaultOpenApi();
builder.AddDefaultAuthentication();

builder.Services.AddControllers();

services.AddLogging();
services.AddTransient(typeof(ILogger), typeof(Logger<Program>));

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});
//services.AddSingleton<IValidator<CreateOrderRequestCommand>, CreateOrderRequestCommandValidator>();

builder.Services.AddDbContext<DeliveryContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveringApiDatabase"), o =>
        {
            o.MigrationsAssembly("FoodDelivery.Delivering.Infrastructure");
        });
});

services.AddTransient<IDeliverySignalRHubService, DeliverySignalRHubService>();

services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<DeliveryContext>>();
services.AddTransient<IDeliveryIntegrationEventService, DeliveryIntegrationEventService>();

services.AddScoped<IDeliveryRepository, DeliveryRepository>();
services.AddScoped<ICourierRepository, CourierRepository>();
services.AddScoped<IAssignDeliveryRepository, AssignDeliveryRepository>();

services.AddSingleton<IAssignDeliveryQueue, AssignDeliveryQueue>();
services.AddSingleton<IUserIdProvider, UserIdProvider>();
services.AddSingleton<IConnectionMapping<string>, ConnectionMapping<string>>();


services.AddSignalR();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();

services.AddHostedService<AssignedDeliveryService>();

var app = builder.Build();
app.UseDefaultOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<DeliveryHub>("/hubs/deliveryHub");
app.MapControllers();

app.Run();
