using DDD.Domain.Models;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
{
    public record CreateDeliveryCommand(
        long OrderId,
        long RecipientId,
        string RecipientName,
        string UserPhone,
        long TotalWeight,
        decimal TotalPrice,
        string PaymentMethod,
        string SenderName,
        string SenderAddress,
        string RecipientAddress,
        string Description
        ) : IRequest<bool>;
    public class CreateDeliveryCommandHandler: IRequestHandler<CreateDeliveryCommand,bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public CreateDeliveryCommandHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
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
