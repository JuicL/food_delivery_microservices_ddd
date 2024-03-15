using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands
{
    public record SetWaitingReceiveDeliveryStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
