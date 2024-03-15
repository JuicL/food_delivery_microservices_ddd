using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetOnDeliveryCourierStatusCommand(long CourierId) : IRequest<bool>;
}
