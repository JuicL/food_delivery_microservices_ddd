using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public record SetArrivedAtDeliveryLocationStatusCommand(long DeliveryId)
        : IRequest<bool>;
}
