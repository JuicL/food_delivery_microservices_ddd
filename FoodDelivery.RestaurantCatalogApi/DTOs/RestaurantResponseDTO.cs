using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;

namespace FoodDelivery.RestaurantCatalogApi.DTOs
{
    public class RestaurantResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BranchResponseDTO>? Branches { get; set; }
    }
}
