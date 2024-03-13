using FoodDelibery.EventBus.Abstractions;
using FoodDelibery.EventBus.Extensions;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using MediatR;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers
{
    public class OrderPaymentSucceededIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderPaymentSucceededIntegrationEventHandler> logger) : IIntegrationEventHandler<OrderPaymentSucceededIntegrationEvent>
    {
        public async Task Handle(OrderPaymentSucceededIntegrationEvent @event)
        {
            logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var command = new SetPaidOrderStatusCommand(@event.OrderId);
            
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
