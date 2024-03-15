using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands
{
    public record SetDeliveredStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
