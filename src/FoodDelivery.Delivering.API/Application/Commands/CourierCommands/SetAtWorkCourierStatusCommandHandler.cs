using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class SetAtWorkCourierStatusCommandHandler : IRequestHandler<SetAtWorkCourierStatusCommand, bool>
    {
        private readonly ICourierRepository _courierRepository;

        public SetAtWorkCourierStatusCommandHandler(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }

        public async Task<bool> Handle(SetAtWorkCourierStatusCommand request, CancellationToken cancellationToken)
        {
            var courier = await _courierRepository.GetByIdAsync(request.CourierId);
            if (courier is null)
                throw new Exception("Courier not found");

            courier.SetAtWorkStatus(WorkAddress.Parse(request.WorkAddress));

            await _courierRepository.UpdateAsync(courier);
            return await _courierRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
