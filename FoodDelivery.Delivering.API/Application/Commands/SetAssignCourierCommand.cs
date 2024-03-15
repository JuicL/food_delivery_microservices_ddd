using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands
{
    public record SetAssignCourierCommand(
        long DeliveryId,
        long CourierId
        ) : IRequest<bool>;

}
