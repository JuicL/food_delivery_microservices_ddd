using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetInProcessOrderStatusCommandHandler : IRequestHandler<SetInProcessOrderStatusCommand, bool>
{
    private readonly IOrderRequestRepository _orderRepository;

    public SetInProcessOrderStatusCommandHandler(IOrderRequestRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(SetInProcessOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);
        if (order is null)
            return false;

        order.SetInProcessStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}