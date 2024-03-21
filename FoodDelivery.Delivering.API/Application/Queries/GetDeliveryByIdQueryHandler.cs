using FoodDelivery.Delivering.API.DTOs;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Queries
{
    public class GetDeliveryByIdQueryHandler : IRequestHandler<GetDeliveryByIdQuery, DeliveryResponceDTO?>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public GetDeliveryByIdQueryHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<DeliveryResponceDTO?> Handle(GetDeliveryByIdQuery request, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.GetByIdAsync(request.DeliveryId);
            if (delivery is null)
                return null;

            return new DeliveryResponceDTO(
                delivery.Id,
                delivery.OrderId,
                delivery.CourierId,
                delivery.RecipientId,
                delivery.RecipientName,
                delivery.UserPhoneNumber.Number,
                delivery.TotalWeight.Grams,
                delivery.TotalPrice.Amount,
                delivery.PaymentMethod.Name,
                delivery.SenderName,
                delivery.SenderAddress.GetFullAddress(),
                delivery.RecipientAddress.GetFullAddress(),
                delivery.Description
                );
        }
    }
}
