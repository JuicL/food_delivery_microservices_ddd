namespace FoodDelivery.OrderApi.DTOs
{
    public class OrderResponseDTO
    {
        public OrderResponseDTO(long id, int userId, string userName,string userPhone, string deliveryAddress, int branchId,
             string restaurantName,string restaurantAddress, string paymentMethod, DateTime orderTime, List<DishesDTO> dishes,string  description)
        {
            Id = id;
            UserId = userId;
            UserName = userName;
            UserPhone = userPhone;
            DeliveryAddress = deliveryAddress;
            BranchId = branchId;
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
            PaymentMethod = paymentMethod;
            OrderTime = orderTime;
            Dishes = dishes;
            Description = description;
        }

        public long Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; }
        public string UserPhone { get; }
        public string DeliveryAddress { get; set; }
        public int BranchId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public DateTime OrderTime { get; set; }
        public List<DishesDTO> Dishes { get; set; }
    }
}
