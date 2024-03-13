using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands;

public class SetAvailabilityConfirmedOrderStatusCommandHadler : IRequestHandler<SetAvailabilityConfirmedOrderStatusCommand, bool>
{
    private readonly IOrderRequestRepository _orderRepository;

    public SetAvailabilityConfirmedOrderStatusCommandHadler(IOrderRequestRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(SetAvailabilityConfirmedOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAsync((int)request.OrderId);
        if (order is null)
            return false;

        order.SetAvailabilityConfirmedStatus();
        return await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}