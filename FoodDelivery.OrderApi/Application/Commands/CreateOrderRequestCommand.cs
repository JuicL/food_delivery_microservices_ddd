using FoodDelivery.OrderApi.DTOs;
using MediatR;

namespace FoodDelivery.OrderApi.Application.Commands
{
    public class CreateOrderRequestCommand : IRequest<long>
    {
        public CreateOrderRequestCommand(int userId, string userName,string phone, string deliveryAddress,
            string restaurantName,
            int branchId, string restaurantAddress, string paymentMethod, DateTime orderTime, List<DishesDTO> dishes,string description)
        {
            UserId = userId;
            UserName = userName;
            Phone = phone;
            DeliveryAddress = deliveryAddress;
            RestaurantName = restaurantName;
            BranchId = branchId;
            RestaurantAddress = restaurantAddress;
            PaymentMethod = paymentMethod;
            Dishes = dishes;
            Description = description;
            OrderTime = orderTime;
        }

        public int UserId { get; }
        public string UserName { get; }
        public string DeliveryAddress { get; }
        public string Phone { get; }
        public string RestaurantName { get; }
        public int BranchId { get; }
        public string RestaurantAddress { get; }
        public string PaymentMethod { get; }
        public DateTime OrderTime { get; }
        public List<DishesDTO> Dishes { get; set; }
        public string Description { get; }
    }
}
