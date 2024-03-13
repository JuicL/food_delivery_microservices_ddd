namespace FoodDelivery.OrderApi.DTOs
{
    public class OrderResponseDTO
    {
        public OrderResponseDTO(long id, int userId, string userName, string deliveryAddress, int branchId, string restaurantAddress, string paymentMethod, DateTime orderTime, List<DishesDTO> dishes)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            DeliveryAddress = deliveryAddress;
            BranchId = branchId;
            RestaurantAddress = restaurantAddress;
            PaymentMethod = paymentMethod;
            OrderTime = orderTime;
            Dishes = dishes;
        }

        public long Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; }
        public string DeliveryAddress { get; set; }
        public int BranchId { get; set; }
        public string RestaurantAddress { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderTime { get; set; }
        public List<DishesDTO> Dishes { get; set; }
    }
}
