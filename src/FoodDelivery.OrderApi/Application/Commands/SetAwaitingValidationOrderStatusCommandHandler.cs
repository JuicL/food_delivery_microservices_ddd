using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetAwaitingValidationOrderStatusCommandHandler : IRequestHandler<SetAwaitingValidationOrderStatusCommand, bool>
{
    private readonly IOrderRequestRepository _orderRepository;

    public SetAwaitingValidationOrderStatusCommandHandler(IOrderRequestRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(SetAwaitingValidationOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync(request.OrderId);
        if (order is null)
            return false;

        order.SetAwaitingValidationStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
