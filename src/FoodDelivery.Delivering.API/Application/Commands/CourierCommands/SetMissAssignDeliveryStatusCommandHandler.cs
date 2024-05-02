using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class SetMissAssignDeliveryStatusCommandHandler : IRequestHandler<SetMissAssignDeliveryStatusCommand, bool>
    {
        private readonly IAssignDeliveryRepository _assignDeliveryRepository;

        public SetMissAssignDeliveryStatusCommandHandler(IAssignDeliveryRepository assignDeliveryRepository)
        {
            _assignDeliveryRepository = assignDeliveryRepository;
        }

        public async Task<bool> Handle(SetMissAssignDeliveryStatusCommand request, CancellationToken cancellationToken)
        {
            var assignDelivery = await _assignDeliveryRepository.GetByCourierAndDeliveryIdsAsync(request.DeliveryId, request.CourierId);
            if (assignDelivery is null)
                throw new Exception("Assigned delivery not found");
            assignDelivery.SetMissStatus();
            await _assignDeliveryRepository.UpdateAsync(assignDelivery);
            return await _assignDeliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
