using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.AgregationModels.ValueObjects;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands
{
    public class CreateOrderRequestCommandHadler : IRequestHandler<CreateOrderRequestCommand,long>
    {
        private readonly IOrderRequestRepository orderRequestRepository;

        public CreateOrderRequestCommandHadler(IOrderRequestRepository orderRequestRepository)
        {
            this.orderRequestRepository = orderRequestRepository;
        }
        public async Task<long> Handle(CreateOrderRequestCommand request, CancellationToken cancellationToken)
        {
            var paymentMethod = Enumeration.GetAll<PaymentMethod>().FirstOrDefault(x => x.Name == request.PaymentMethod);
            if (paymentMethod is null)
                throw new Exception("Invalid payment method");

            var order = new OrderRequest(request.UserId, request.UserName,Phone.ParseFromInternational(request.Phone) ,Address.Parse(request.DeliveryAddress), request.BranchId,
                Address.Parse(request.RestaurantAddress), paymentMethod,request.OrderTime,request.RestaurantName, new List<Dishes>(), request.Description);
            foreach (var item in request.Dishes)
            {
                var dish = new Dishes(item.Id, item.Name, new Weight(item.Weight), new Price(item.Price), item.Units);
                order.AddDishes(dish);
            }

            await orderRequestRepository.CreateAsync(order);

            await orderRequestRepository.UnitOfWork.SaveEntitiesAsync();
            return order.Id;
        }
    }
}
