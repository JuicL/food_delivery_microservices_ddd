using FoodDelivery.PaymentProcessor.IntegrationEvents.EventHandling;
using FoodDelivery.PaymentProcessor.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.AddRabbitMqEventBus("EventBus")
    .AddSubscription<OrderStatusChangedToAvailabilityConfirmedIntegrationEvent, OrderStatusChangedToAvailabilityConfirmedIntegrationEventHandler>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
