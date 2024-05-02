namespace FoodDelivery.OrderApi.DTOs
{
    public class DishesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Weight { get; set; }
        public decimal Price { get; set; }
        public int Units { get; set; }
    }
}
