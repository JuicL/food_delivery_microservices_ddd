namespace FoodDelivery.OrderApi.DTOs
{
    public class CreateOrderRequestDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string DeliveryAddress { get; set; }
        public int BranchId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime OrderTime { get; set; }
        public List<DishesDTO> Dishes { get; set; }
        public string Description { get; set; }
    }
}
