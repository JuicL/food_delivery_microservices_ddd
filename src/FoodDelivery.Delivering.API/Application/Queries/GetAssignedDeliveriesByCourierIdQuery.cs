using FoodDelivery.Delivering.API.DTOs;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using MediatR;

namespace FoodDelivery.Delivering.API.Application.Queries
{
    public record GetAssignedDeliveriesByCourierIdQuery(long CourierId) : IRequest<List<AssignDeliveryDTO>>;
    public class GetAssignedDeliveriesByCourierIdQueryHandler : IRequestHandler<GetAssignedDeliveriesByCourierIdQuery, List<AssignDeliveryDTO>>
    {
        private readonly IAssignDeliveryRepository _assignDeliveryRepository;

        public GetAssignedDeliveriesByCourierIdQueryHandler(IAssignDeliveryRepository assignDeliveryRepository)
        {
            _assignDeliveryRepository = assignDeliveryRepository;
        }

        public async Task<List<AssignDeliveryDTO>> Handle(GetAssignedDeliveriesByCourierIdQuery request, CancellationToken cancellationToken)
        {
            var deliveries = await _assignDeliveryRepository.GetByCourierIdAsync(request.CourierId);
            return deliveries.Select(x =>
            new AssignDeliveryDTO()
            {
                CourierId = x.CourierId,
                Status = x.Status.Name,
                Delivery = new DeliveryResponceDTO(
                    x.Delivery.Id,
                    x.Delivery.OrderId,
                    x.Delivery.CourierId,
                    x.Delivery.RecipientId,
                    x.Delivery.RecipientName,
                    x.Delivery.UserPhoneNumber.Number,
                    x.Delivery.TotalWeight.Grams,
                    x.Delivery.TotalPrice.Amount,
                    x.Delivery.PaymentMethod.Name,
                    x.Delivery.SenderName,
                    x.Delivery.SenderAddress.GetFullAddress(),
                    x.Delivery.RecipientAddress.GetFullAddress(),
                    x.Delivery.Description
                    )
            }
            ).ToList();
        }
    }

}
