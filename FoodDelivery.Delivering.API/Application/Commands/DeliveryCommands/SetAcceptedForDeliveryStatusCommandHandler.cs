using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public class SetAcceptedForDeliveryStatusCommandHandler : IRequestHandler<SetAcceptedForDeliveryStatusCommand, bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public SetAcceptedForDeliveryStatusCommandHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(SetAcceptedForDeliveryStatusCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(request.DeliveryId);
            if (delivery is null)
                throw new Exception("Delivery not found");
            delivery.SetAcceptedForDeliveryStatus();
            await _deliveryRepository.UpdateAsync(delivery);
            return await _deliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
