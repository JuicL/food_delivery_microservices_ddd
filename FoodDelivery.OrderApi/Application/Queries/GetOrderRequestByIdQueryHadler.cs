using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.DTOs;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Queries
{
    public class GetOrderRequestByIdQueryHadler : IRequestHandler<GetOrderRequestByIdQuery, OrderResponseDTO>
    {
        private readonly IOrderRequestRepository orderRequestRepository;

        public GetOrderRequestByIdQueryHadler(IOrderRequestRepository orderRequestRepository)
        {
            this.orderRequestRepository = orderRequestRepository;
        }
        public async Task<OrderResponseDTO> Handle(GetOrderRequestByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRequestRepository.GetAsync(request.OrderId);
            if (order is null)
                throw new Exception("Order not found");

            var orderResponse = new OrderResponseDTO(order.Id,order.UserId,order.UserName,order.Phone.Number,order.DeliveryAddress.GetFullAddress(),
                order.BranchId,order.RestaurantName,order.RestaurantAddress.GetFullAddress(),order.PaymentMethod.Name,order.OrderTime,
                order.Dishes.Select(x=> new DishesDTO() { Id = x.DishId, Name = x.Name, Price = x.Price.Amount, Weight = x.Weight.Grams}).ToList()
                ,order.Description);
            return orderResponse;
        }
    }
}
