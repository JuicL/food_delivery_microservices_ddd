using FoodDelibery.EventBus.Events;
using FoodDelivery.OrderApi.DTOs;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

public record class OrderStatusChangedToInProcessDeliveryIntegrationEvent : IntegrationEvent
{
    private long id;
    private int branchId;
    private List<OrderItem> orderItems;

    public OrderStatusChangedToInProcessDeliveryIntegrationEvent(long id, int branchId, List<OrderItem> orderItems)
    {
        this.id = id;
        this.branchId = branchId;
        this.orderItems = orderItems;
    }
}
