namespace FoodDelivery.DeliveringAPI.Extentions
{
    public static class Extentions
    {
        public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            //eventBus.AddSubscription<OrderAvailabilityConfirmedIntegrationEvent, OrderAvailabilityConfirmedIntegrationEventHandler>();
        }
    }
}
