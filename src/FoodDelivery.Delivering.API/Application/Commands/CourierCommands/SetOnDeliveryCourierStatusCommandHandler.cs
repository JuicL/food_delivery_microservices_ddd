using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class SetOnDeliveryCourierStatusCommandHandler : IRequestHandler<SetOnDeliveryCourierStatusCommand, bool>
    {
        private readonly ICourierRepository _courierRepository;

        public SetOnDeliveryCourierStatusCommandHandler(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task<bool> Handle(SetOnDeliveryCourierStatusCommand request, CancellationToken cancellationToken)
        {
            var courier = await _courierRepository.GetByIdAsync(request.CourierId);
            if (courier is null)
                throw new Exception("Courier not found");

            courier.SetOnDeliveryStatus();

            await _courierRepository.UpdateAsync(courier);
            return await _courierRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
