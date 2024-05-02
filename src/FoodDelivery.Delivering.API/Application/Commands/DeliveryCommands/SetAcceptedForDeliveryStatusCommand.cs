using FoodDelivery.Delivering.API.Application.Commands.CourierCommands;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public record SetAcceptedForDeliveryStatusCommand(long DeliveryId)
        : IRequest<bool>;
}
