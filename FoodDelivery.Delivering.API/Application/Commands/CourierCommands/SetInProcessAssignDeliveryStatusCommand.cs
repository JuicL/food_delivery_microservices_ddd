using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record SetInProcessAssignDeliveryStatusCommand(
        long DeliveryId,
        long CourierId
        ) : IRequest<bool>;  
    
}
