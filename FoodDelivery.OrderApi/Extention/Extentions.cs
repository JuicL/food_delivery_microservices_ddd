using FoodDelivery.Delivering.API.Application.IntegrationEvents.Events;
using FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;

namespace FoodDelivery.OrderApi.Extention
{
    public static class Extentions
    {
        public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            eventBus.AddSubscription<OrderAvailabilityConfirmedIntegrationEvent,OrderAvailabilityConfirmedIntegrationEventHandler>();
            eventBus.AddSubscription<OrderPaymentFailedIntegrationEvent,OrderPaymentFailedIntegrationEventHandler>();
            eventBus.AddSubscription<OrderPaymentSucceededIntegrationEvent,OrderPaymentSucceededIntegrationEventHandler>();
            eventBus.AddSubscription<OrderRejectedIntegrationEvent,OrderRejectedIntegrationEventHandler>();
            eventBus.AddSubscription<OrderStatusChangedToCreatedIntegrationEvent, OrderStatusChangedToCreatedIntegrationEventHandler>();
            
            eventBus.AddSubscription<DeliveryStatusChangedToCanceledIntegrationEvent, OrderCanceledIntegrationEventHandler>();
            eventBus.AddSubscription<DeliveryStatusChangedToDeliveredIntegrationEvent, OrderDeliveredIntegrationEventHandler>();
        }
    }
}
