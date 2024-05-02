using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetAvailabilityConfirmedOrderStatusCommand : IRequest<bool>
{
    public long OrderId { get; }

    public SetAvailabilityConfirmedOrderStatusCommand(long orderId)
    {
        OrderId = orderId;
    }
}
