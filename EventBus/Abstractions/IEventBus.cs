using System.Threading.Tasks;

namespace FoodDelivery.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}
