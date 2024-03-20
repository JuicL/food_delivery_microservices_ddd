using FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.CourierCommands
{
    public class CreateCourierCommandHandler : IRequestHandler<CreateCourierCommand,bool>
    {
        private readonly ICourierRepository _courierRepository;

        public CreateCourierCommandHandler(ICourierRepository courierRepository)
        {
            _courierRepository = courierRepository;
        }

        async Task<bool> IRequestHandler<CreateCourierCommand, bool>.Handle(CreateCourierCommand request, CancellationToken cancellationToken)
        {
            var courier = new Courier(
                request.CourierId, 
                request.UserName,
                Phone.ParseFromInternational(request.PhoneNumber), 
                WorkAddress.Parse(request.WorkAddress));

            await _courierRepository.CreateAsync(courier);
            return await _courierRepository.UnitOfWork.SaveEntitiesAsync();
        }
    }
}
