using FoodDelibery.EventBus.Abstractions;
using FoodDelibery.EventBus.Events;
using FoodDelibery.EventBus.Extensions;
using FoodDelivery.OrderApi.Application.Commands;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.Events
{
    public record DeliveryStatusChangedToCanceledIntegrationEvent(long DeliveryId,long OrderId) : IntegrationEvent;

    public class OrderCanceledIntegrationEventHandler(
        IMediator mediator,
        ILogger<DeliveryStatusChangedToCanceledIntegrationEvent> logger) :
        IIntegrationEventHandler<DeliveryStatusChangedToCanceledIntegrationEvent>
    {
        public async Task Handle(DeliveryStatusChangedToCanceledIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var command = new SetCanceledOrderStatusCommand(@event.OrderId);

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