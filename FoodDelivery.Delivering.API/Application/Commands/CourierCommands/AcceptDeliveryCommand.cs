using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record AcceptDeliveryCommand(
        long DeliveryId,
        long CourierId
        ) : IRequest<bool>;
}
