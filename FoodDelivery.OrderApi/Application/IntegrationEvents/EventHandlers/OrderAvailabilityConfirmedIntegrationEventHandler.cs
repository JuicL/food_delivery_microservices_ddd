using FoodDelibery.EventBus.Abstractions;
using FoodDelibery.EventBus.Extensions;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using MediatR;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers
{
    public class OrderAvailabilityConfirmedIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderAvailabilityConfirmedIntegrationEventHandler> logger) : IIntegrationEventHandler<OrderAvailabilityConfirmedIntegrationEvent>
    {
        public async Task Handle(OrderAvailabilityConfirmedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var command = new SetAvailabilityConfirmedOrderStatusCommand(@event.OrderId);

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
