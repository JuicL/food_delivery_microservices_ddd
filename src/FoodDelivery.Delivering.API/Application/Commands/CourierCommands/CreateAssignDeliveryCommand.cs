using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public record CreateAssignDeliveryCommand(long DeliveryId,
        long CourierId
        ) : IRequest<bool>;

    public class CreateAssignDeliveryCommandHandler : IRequestHandler<CreateAssignDeliveryCommand, bool>
    {
        private readonly IAssignDeliveryRepository _assignDeliveryRepository;

        public CreateAssignDeliveryCommandHandler(IAssignDeliveryRepository assignDeliveryRepository)
        {
            _assignDeliveryRepository = assignDeliveryRepository;
        }

        public async Task<bool> Handle(CreateAssignDeliveryCommand request, CancellationToken cancellationToken)
        {
            var assignDelivery = new AssignDelivery(request.DeliveryId, request.CourierId);
            await _assignDeliveryRepository.CreateAsync(assignDelivery);
            return await _assignDeliveryRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
