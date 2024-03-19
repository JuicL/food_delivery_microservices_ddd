using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public class SetArrivedAtDeliveryLocationStatusCommandHandler : IRequestHandler<SetArrivedAtDeliveryLocationStatusCommand, bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public SetArrivedAtDeliveryLocationStatusCommandHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(SetArrivedAtDeliveryLocationStatusCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(request.DeliveryId);
            if (delivery is null)
                throw new Exception("Delivery not found");
            delivery.SetArrivedAtDeliveryLocationStatus();
            await _deliveryRepository.UpdateAsync(delivery);
            return await _deliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
