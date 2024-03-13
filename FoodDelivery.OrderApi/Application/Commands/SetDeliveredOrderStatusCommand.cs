using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetDeliveredOrderStatusCommand : IRequest<bool>
{
    public long OrderId { get; }

    public SetDeliveredOrderStatusCommand(long orderId)
    {
        OrderId = orderId;
    }
}
