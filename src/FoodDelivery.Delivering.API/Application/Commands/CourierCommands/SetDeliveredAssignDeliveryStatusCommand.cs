using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetDeliveredAssignDeliveryStatusCommand(
        long DeliveryId,
        long CourierId
        ) : IRequest<bool>;
}
