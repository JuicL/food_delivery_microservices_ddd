using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetDeliveredOrderStatusCommandHandler : IRequestHandler<SetDeliveredOrderStatusCommand, bool>
{
    private readonly IOrderRequestRepository _orderRepository;

    public SetDeliveredOrderStatusCommandHandler(IOrderRequestRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(SetDeliveredOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);
        if (order is null)
            return false;

        order.SetDeliveredStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
