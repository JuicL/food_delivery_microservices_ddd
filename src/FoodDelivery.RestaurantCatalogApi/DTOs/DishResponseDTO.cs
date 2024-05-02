using Microsoft.OpenApi.Models;

namespace FoodDelivery.RestaurantCatalogApi.DTOs
{
    public class DishResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public long Weight { get; set; }
        public decimal Price { get; set; }
        public string DishType { get; set; }
        public List<string> Ingredients { get; set; }
    }  
    public class DishAvaibleResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } 
        public long Weight { get; set; }
        public decimal Price { get; set; }
        public string DishType { get; set; }
        public List<string> Ingredients { get; set; }
        public bool IsAvaible { get; set; }
    }
}
