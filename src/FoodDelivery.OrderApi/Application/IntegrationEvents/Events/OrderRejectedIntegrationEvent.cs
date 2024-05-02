using FoodDelivery.EventBus.Events;
using FoodDelivery.RestaurantCatalogApi.DTOs;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record OrderRejectedIntegrationEvent(long OrderId, List<DishAvaibilityDTO> OutStockItems) : IntegrationEvent;

