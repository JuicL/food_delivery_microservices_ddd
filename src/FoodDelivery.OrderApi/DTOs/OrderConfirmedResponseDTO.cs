namespace FoodDelivery.RestaurantCatalogApi.DTOs
{
    public class OrderConfirmedResponseDTO
    {
        public long OrderId {  get; set; }
        public List<DishAvaibilityDTO> Dishes { get; set; }
    }

    public class DishAvaibilityDTO
    {
        public long DishId { get; set;}
        public bool IsAvaible { get; set;}
    }
}
