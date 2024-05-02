using FoodDelivery.EventBus.Events;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents
{
    public interface IOrderIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
    
}
