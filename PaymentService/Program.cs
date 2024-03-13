using FoodDelibery.PaymentProcessor.IntegrationEvents.EventHandling;
using FoodDelibery.PaymentProcessor.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.AddRabbitMqEventBus("EventBus")
    .AddSubscription<OrderStatusChangedToAvailabilityConfirmedIntegrationEvent, OrderStatusChangedToAvailabilityConfirmedIntegrationEventHandler>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
