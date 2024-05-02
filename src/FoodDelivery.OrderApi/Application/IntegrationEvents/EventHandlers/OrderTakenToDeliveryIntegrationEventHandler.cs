using FoodDelivery.EventBus.Abstractions;
using FoodDelivery.EventBus.Extensions;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using MediatR;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers
{
    public class OrderTakenToDeliveryIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderTakenToDeliveryIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderTakenToDeliveryIntegrationEvent>
    {
        public async Task Handle(OrderTakenToDeliveryIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var command = new SetInProcessOrderStatusCommand(@event.OrderId);

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
