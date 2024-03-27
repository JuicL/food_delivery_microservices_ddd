using FoodDelivery.EventBus.Events;
using FoodDelivery.OrderApi.DTOs;

namespace FoodDelivery.Delivering.Application.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent(OrderResponseDTO Order) : IntegrationEvent;
