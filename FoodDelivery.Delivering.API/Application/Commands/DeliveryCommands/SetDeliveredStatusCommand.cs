using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public record SetDeliveredStatusCommand(long DeliveryId)
        : IRequest<bool>;

}
