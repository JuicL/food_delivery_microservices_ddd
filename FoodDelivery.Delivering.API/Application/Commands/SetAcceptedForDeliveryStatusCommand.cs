using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands
{
    public record SetAcceptedForDeliveryStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
