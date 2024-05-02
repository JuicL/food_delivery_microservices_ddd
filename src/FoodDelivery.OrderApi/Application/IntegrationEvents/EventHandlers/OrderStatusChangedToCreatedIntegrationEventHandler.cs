using FoodDelivery.EventBus.Abstractions;
using FoodDelivery.EventBus.Extensions;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using MediatR;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers
{
    public class OrderStatusChangedToCreatedIntegrationEventHandler(
    IMediator _mediator,
    ILogger<OrderStatusChangedToCreatedIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToCreatedIntegrationEvent>
    {
        public async Task Handle(OrderStatusChangedToCreatedIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var command = new SetAwaitingValidationOrderStatusCommand(@event.OrderId);

            logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                command.GetGenericTypeName(),
                nameof(command.OrderId),
                command.OrderId,
                command);

            await _mediator.Send(command);
        }
    }    
}
