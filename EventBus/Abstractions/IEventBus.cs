using System.Threading.Tasks;

namespace FoodDelibery.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}
