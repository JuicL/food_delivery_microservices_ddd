using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands
{
    public record SetArrivedAtDeliveryLocationStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
