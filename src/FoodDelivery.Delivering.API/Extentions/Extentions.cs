using FoodDelivery.IntegrationEventLogEF.Services;
using FoodDelivery.Delivering.API.Application.Services;
using FoodDelivery.Delivering.API.Application.Services.SignalR;
using FoodDelivery.Delivering.Application.Behaviors;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using FoodDelivery.Delivering.Infrastructure;
using FoodDelivery.Delivering.Infrastructure.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.DeliveringAPI.Extentions
{
    public static class Extentions
    {
        public static IHostApplicationBuilder AddAplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddMediatR(cfg =>
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

            builder.Services.AddTransient<IDeliverySignalRHubService, DeliverySignalRHubService>();
            builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<DeliveryContext>>();
            builder.Services.AddTransient<IDeliveryIntegrationEventService, DeliveryIntegrationEventService>();
            builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            builder.Services.AddScoped<ICourierRepository, CourierRepository>();
            builder.Services.AddScoped<IAssignDeliveryRepository, AssignDeliveryRepository>();
            builder.Services.AddSingleton<IAssignDeliveryQueue, AssignDeliveryQueue>();
            builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();
            builder.Services.AddSingleton<IConnectionMapping<string>, ConnectionMapping<string>>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            });
            builder.Services.AddHostedService<AssignedDeliveryService>();
            return builder;
        }

        public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            //eventBus.AddSubscription<OrderAvailabilityConfirmedIntegrationEvent, OrderAvailabilityConfirmedIntegrationEventHandler>();
        }
    }
}
