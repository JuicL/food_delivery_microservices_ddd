using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetOnDeliveryCourierStatusCommand(long CourierId) : IRequest<bool>;
}
