using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class SetInProcessAssignDeliveryStatusCommandHandler : IRequestHandler<SetInProcessAssignDeliveryStatusCommand, bool>
    {
        private readonly IAssignDeliveryRepository _assignDeliveryRepository;

        public SetInProcessAssignDeliveryStatusCommandHandler(IAssignDeliveryRepository assignDeliveryRepository)
        {
            _assignDeliveryRepository = assignDeliveryRepository;
        }

        public async Task<bool> Handle(SetInProcessAssignDeliveryStatusCommand request, CancellationToken cancellationToken)
        {
            var assignDelivery = await _assignDeliveryRepository.GetByCourierAndDeliveryIdsAsync(request.DeliveryId, request.CourierId);
            if (assignDelivery is null)
                throw new Exception("Assigned delivery not found");
            assignDelivery.SetInProcessStatus();
            await _assignDeliveryRepository.UpdateAsync(assignDelivery);
            return await _assignDeliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
