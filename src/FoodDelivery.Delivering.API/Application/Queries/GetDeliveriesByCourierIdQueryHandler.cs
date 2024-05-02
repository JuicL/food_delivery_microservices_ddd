using FoodDelivery.Delivering.API.DTOs;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Queries
{
    public class GetDeliveriesByCourierIdQueryHandler : IRequestHandler<GetDeliveriesByCourierIdQuery, List<DeliveryResponceDTO>>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        public GetDeliveriesByCourierIdQueryHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<List<DeliveryResponceDTO>> Handle(GetDeliveriesByCourierIdQuery request, CancellationToken cancellationToken)
        {
            var deliveries = await _deliveryRepository.GetByCourierIdAsync(request.CourierId);
            
            return deliveries.Select(x => new DeliveryResponceDTO(
                x.Id,
                x.OrderId,
                x.CourierId,
                x.RecipientId,
                x.RecipientName,
                x.UserPhoneNumber.Number,
                x.TotalWeight.Grams,
                x.TotalPrice.Amount,
                x.PaymentMethod.Name,
                x.SenderName,
                x.SenderAddress.GetFullAddress(),
                x.RecipientAddress.GetFullAddress(),
                x.Description
                )).ToList();
        }
    }
}
