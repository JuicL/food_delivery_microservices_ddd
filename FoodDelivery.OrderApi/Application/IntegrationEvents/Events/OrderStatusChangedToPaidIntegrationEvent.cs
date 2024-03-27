using FoodDelivery.EventBus.Events;
using FoodDelivery.OrderApi.DTOs;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent(OrderResponseDTO Order) : IntegrationEvent;