using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public record SetWaitingReceiveDeliveryStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
