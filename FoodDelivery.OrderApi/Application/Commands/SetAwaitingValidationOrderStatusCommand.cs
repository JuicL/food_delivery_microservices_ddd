using FoodDelivery.OrderApi.Application.Commands;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetAwaitingValidationOrderStatusCommand : IRequest<bool>
{
    public long OrderId { get; }

    public SetAwaitingValidationOrderStatusCommand(long orderId)
    {
        OrderId = orderId;
    }
}
