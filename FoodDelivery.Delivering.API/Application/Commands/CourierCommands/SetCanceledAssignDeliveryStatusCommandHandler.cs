using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class SetCanceledAssignDeliveryStatusCommandHandler : IRequestHandler<SetCanceledAssignDeliveryStatusCommand, bool>
    {
        private readonly IAssignDeliveryRepository _assignDeliveryRepository;

        public SetCanceledAssignDeliveryStatusCommandHandler( IAssignDeliveryRepository assignDeliveryRepository)
        {
            _assignDeliveryRepository = assignDeliveryRepository;
        }

        public async Task<bool> Handle(SetCanceledAssignDeliveryStatusCommand request, CancellationToken cancellationToken)
        {
            var assignDelivery = await _assignDeliveryRepository.GetByCourierAndDeliveryIdsAsync(request.DeliveryId,request.CourierId);
            if (assignDelivery is null)
                throw new Exception("Assigned delivery not found");
            assignDelivery.SetCanceledStatus();
            await _assignDeliveryRepository.UpdateAsync(assignDelivery);
            return await _assignDeliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
