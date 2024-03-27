using FoodDelivery.EventBus.Abstractions;
using FoodDelivery.EventBus.Events;
using FoodDelivery.PaymentProcessor.IntegrationEvents.Events;
using Microsoft.Extensions.Options;

namespace FoodDelivery.PaymentProcessor.IntegrationEvents.EventHandling;

public class OrderStatusChangedToAvailabilityConfirmedIntegrationEventHandler(
    IEventBus eventBus,
    IOptionsMonitor<PaymentOptions> options,
    ILogger<OrderStatusChangedToAvailabilityConfirmedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToAvailabilityConfirmedIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToAvailabilityConfirmedIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        IntegrationEvent orderPaymentIntegrationEvent;


        // Here we're simulating that we'd be performing the payment against any payment gateway
        // Instead of a real payment we just take the env. var to simulate the payment 
        // The payment can be successful or it can fail

        if (options.CurrentValue.PaymentSucceeded)
        {
            orderPaymentIntegrationEvent = new OrderPaymentSucceededIntegrationEvent(@event.OrderId);
        }
        else
        {
            orderPaymentIntegrationEvent = new OrderPaymentFailedIntegrationEvent(@event.OrderId);
        }

        logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", orderPaymentIntegrationEvent.Id, orderPaymentIntegrationEvent);

        await eventBus.PublishAsync(orderPaymentIntegrationEvent);
    }
}
