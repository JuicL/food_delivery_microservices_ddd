using FoodDelibery.EventBus.Events;
using FoodDelivery.RestaurantCatalogApi.DTOs;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events
{
    public record OrderStockRejectedIntegrationEvent(long OrderId, List<DishAvaibilityDTO> OutStockItems) : IntegrationEvent;
}
