using FoodDelibery.EventBus.Abstractions;
using FoodDelibery.EventBus.Events;
using FoodDelibery.EventBus.Extensions;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record DeliveryStatusChangedToDeliveredIntegrationEvent(long DeliveryId,long OrderId) : IntegrationEvent;
    
    public class OrderDeliveredIntegrationEventHandler(
    IMediator mediator,
    ILogger<DeliveryStatusChangedToDeliveredIntegrationEvent> logger) :
    IIntegrationEventHandler<DeliveryStatusChangedToDeliveredIntegrationEvent>
    {
        public async Task Handle(DeliveryStatusChangedToDeliveredIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var command = new SetDeliveredOrderStatusCommand(@event.OrderId);

            logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetGenericTypeName(),
                nameof(command.OrderId),
                command.OrderId,
                command);

            await mediator.Send(command);
        }
    }
}