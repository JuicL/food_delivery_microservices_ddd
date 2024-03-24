using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class AcceptDeliveryCommandHandler : IRequestHandler<AcceptDeliveryCommand, bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IAssignDeliveryRepository _assignDeliveryRepository;

        public AcceptDeliveryCommandHandler(IDeliveryRepository deliveryRepository, IAssignDeliveryRepository assignDeliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
            _assignDeliveryRepository = assignDeliveryRepository;
        }

        public async Task<bool> Handle(AcceptDeliveryCommand request, CancellationToken cancellationToken)
        {
            var assignDelivery = await _assignDeliveryRepository.GetByCourierAndDeliveryIdsAsync(request.DeliveryId, request.CourierId);
            if (assignDelivery is null)
                throw new Exception("Assign delivery not found");
            if (assignDelivery.Status != AssignDeliveryStatus.WaitingConfirm)
                throw new Exception("Invalid request");

            assignDelivery.SetInProcessStatus();
            await _assignDeliveryRepository.UpdateAsync(assignDelivery);
            return await _assignDeliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    } 
}
