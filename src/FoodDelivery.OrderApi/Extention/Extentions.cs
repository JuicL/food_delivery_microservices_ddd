using FluentValidation;
using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.IntegrationEventLogEF.Services;
using FoodDelivery.OrderApi.Application.Behaviors;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Application.Validations;
using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate;
using FoodDelivery.OrderApi.Infrastructure;
using FoodDelivery.OrderApi.Infrastructure.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.OrderApi.Extention
{
    public static class Extentions
    {
        public static IHostApplicationBuilder AddAplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
                cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
                cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
                cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            });

            builder.Services.AddSingleton<IValidator<CreateOrderRequestCommand>, CreateOrderRequestCommandValidator>();

            builder.Services.AddDbContext<OrderingContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("OrderingApiDatabase"),
                    b => b.MigrationsAssembly("FoodDelivery.OrderApi.Infrastructure"));
            });

            builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<OrderingContext>>();
            builder.Services.AddTransient<IOrderIntegrationEventService, OrderIntegrationEventService>();
            builder.Services.AddScoped<IOrderRequestRepository, OrderRequestRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            return builder;
        }

        public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            eventBus.AddSubscription<OrderAvailabilityConfirmedIntegrationEvent,OrderAvailabilityConfirmedIntegrationEventHandler>();
            eventBus.AddSubscription<OrderPaymentFailedIntegrationEvent,OrderPaymentFailedIntegrationEventHandler>();
            eventBus.AddSubscription<OrderPaymentSucceededIntegrationEvent,OrderPaymentSucceededIntegrationEventHandler>();
            eventBus.AddSubscription<OrderRejectedIntegrationEvent,OrderRejectedIntegrationEventHandler>();
            eventBus.AddSubscription<OrderStatusChangedToCreatedIntegrationEvent, OrderStatusChangedToCreatedIntegrationEventHandler>();
            
            eventBus.AddSubscription<DeliveryStatusChangedToCanceledIntegrationEvent, OrderCanceledIntegrationEventHandler>();
            eventBus.AddSubscription<DeliveryStatusChangedToDeliveredIntegrationEvent, OrderDeliveredIntegrationEventHandler>();
        }
    }
}
