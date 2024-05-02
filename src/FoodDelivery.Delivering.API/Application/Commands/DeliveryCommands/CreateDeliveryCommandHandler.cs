using DDD.Domain.Models;
using FoodDelivery.Delivering.API.Application.Services;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public class CreateDeliveryCommandHandler: IRequestHandler<CreateDeliveryCommand,bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IAssignDeliveryQueue _assignDeliveryQueue;

        public CreateDeliveryCommandHandler(IDeliveryRepository deliveryRepository,
            IAssignDeliveryQueue assignDeliveryQueue)
        {
            _deliveryRepository = deliveryRepository;
            _assignDeliveryQueue = assignDeliveryQueue;
        }

        public async Task<bool> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken)
        {
            var delivery = new Delivery(
                request.OrderId,
                request.RecipientId,
                request.RecipientName,
                Phone.ParseFromInternational(request.UserPhone),
                new Weight(request.TotalWeight),
                new Price(request.TotalPrice),
                Enumeration.GetAll<PaymentMethod>().Single(x=> x.Name == request.PaymentMethod),
                request.SenderName,
                Address.Parse(request.SenderAddress),
                Address.Parse(request.RecipientAddress),
                request.Description
                );

            await _deliveryRepository.CreateAsync(delivery);
            
            

            return await _deliveryRepository.UnitOfWork.SaveEntitiesAsync();
            
        }
    }
}
