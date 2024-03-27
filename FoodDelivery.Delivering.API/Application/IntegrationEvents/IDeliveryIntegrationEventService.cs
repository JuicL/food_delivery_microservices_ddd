using FoodDelivery.EventBus.Events;

namespace FoodDelivery.Delivering.Application.IntegrationEvents
{
    public interface IDeliveryIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
    
}
