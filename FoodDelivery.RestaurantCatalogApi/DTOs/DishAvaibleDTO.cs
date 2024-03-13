namespace FoodDelivery.RestaurantCatalogApi.DTOs
{
    public class DishAvaibleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Weight { get; set; }
        public decimal Price { get; set; }
        public string DishType { get; set; }
        public List<string> Ingredients { get; set; }
        public bool IsAvaible { get; set; }
    }
}
