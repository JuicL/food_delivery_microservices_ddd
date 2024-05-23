using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetAtWorkCourierStatusCommand(long CourierId, string WorkAddress) : IRequest<bool>;
}
