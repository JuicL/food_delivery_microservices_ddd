using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetWorkOffCourierStatusCommand(long CourierId) : IRequest<bool>;
}
