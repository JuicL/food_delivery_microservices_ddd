using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands
{
    public record SetCanceledStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
