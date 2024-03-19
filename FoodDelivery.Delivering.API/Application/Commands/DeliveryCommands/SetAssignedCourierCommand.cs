using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetAssignedCourierCommand(
        long DeliveryId,
        long CourierId
        ) : IRequest<bool>;
}
