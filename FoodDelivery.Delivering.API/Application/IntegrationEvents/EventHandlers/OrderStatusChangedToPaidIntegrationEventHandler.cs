using FoodDelibery.EventBus.Abstractions;
using FoodDelibery.EventBus.Extensions;
using FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands;
using FoodDelivery.Delivering.Application.IntegrationEvents.Events;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.IntegrationEvents.EventHandlers;

public class OrderStatusChangedToPaidIntegrationEventHandler(
    IMediator mediator,
    ILogger<OrderStatusChangedToPaidIntegrationEvent> logger) : IIntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);
        var order = @event.Order;

        var command = new CreateDeliveryCommand(
            order.Id,
            order.UserId,
            order.UserName,
            order.UserPhone,
            order.Dishes.Sum(x=> x.Weight * x.Units),
            order.Dishes.Sum(x=> x.Price * x.Units),
            order.PaymentMethod,
            order.RestaurantName,
            order.RestaurantAddress,
            order.DeliveryAddress,
            order.Description
            );;

        logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            command.GetGenericTypeName(),
            nameof(command.OrderId),
            command.OrderId,
            command);

        await mediator.Send(command);
    }
}