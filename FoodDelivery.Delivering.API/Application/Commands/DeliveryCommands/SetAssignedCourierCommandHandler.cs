using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class SetAssignedCourierCommandHandler : IRequestHandler<SetAssignedCourierCommand, bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public SetAssignedCourierCommandHandler(IDeliveryRepository deliveryRepository, IAssignDeliveryRepository assignDeliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(SetAssignedCourierCommand request, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(request.DeliveryId);
            if (delivery is null)
                throw new Exception("Delivery not found");
            delivery.AssignCourier(request.CourierId);
            await _deliveryRepository.UpdateAsync(delivery);
            return await _deliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
