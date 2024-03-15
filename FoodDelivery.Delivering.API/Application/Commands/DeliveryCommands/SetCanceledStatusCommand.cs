using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public record SetCanceledStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
