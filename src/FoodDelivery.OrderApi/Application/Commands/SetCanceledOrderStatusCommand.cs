using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetCanceledOrderStatusCommand : IRequest<bool>
{
    public long OrderId { get; }

    public SetCanceledOrderStatusCommand(long orderId)
    {
        OrderId = orderId;
    }
}
