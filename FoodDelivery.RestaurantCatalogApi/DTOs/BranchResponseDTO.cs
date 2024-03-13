using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;

namespace FoodDelivery.RestaurantCatalogApi.DTOs
{
    public class BranchResponseDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int RestaurantId { get; set; }
        public TimeOnly OpenningTime { get; set; }
        public TimeOnly ClosingTime { get; set; }
        public bool IsAvaible { get; set; }
        public List<DishAvaibleResponseDTO>? Dishes { get; set; }
    }
}
