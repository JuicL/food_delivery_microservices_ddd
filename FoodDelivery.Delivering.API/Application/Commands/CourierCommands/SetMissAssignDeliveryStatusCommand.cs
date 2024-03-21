using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetMissAssignDeliveryStatusCommand(long DeliveryId,long CourierId) : IRequest<bool>;
}
