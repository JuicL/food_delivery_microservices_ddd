using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetInProcessOrderStatusCommand : IRequest<bool>
{
    public long OrderId { get; }

    public SetInProcessOrderStatusCommand(long orderId)
    {
        OrderId = orderId;
    }
}
