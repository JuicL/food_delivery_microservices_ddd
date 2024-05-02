using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetCanceledOrderStatusCommandHandler : IRequestHandler<SetCanceledOrderStatusCommand, bool>
{
    private readonly IOrderRequestRepository _orderRepository;

    public SetCanceledOrderStatusCommandHandler(IOrderRequestRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(SetCanceledOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);
        if (order is null)
            return false;

        order.SetCanceleStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
