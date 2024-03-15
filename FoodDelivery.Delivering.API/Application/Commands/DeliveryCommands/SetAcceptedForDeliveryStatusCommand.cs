using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public record SetAcceptedForDeliveryStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
