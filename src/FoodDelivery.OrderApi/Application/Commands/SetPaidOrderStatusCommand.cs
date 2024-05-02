using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetPaidOrderStatusCommand : IRequest<bool>
{
    public long OrderId { get; }

    public SetPaidOrderStatusCommand(long orderId)
    {
        OrderId = orderId;
    }
}
