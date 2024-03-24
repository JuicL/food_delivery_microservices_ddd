namespace FoodDelivery.Delivering.API.Application.Services
{
    public interface IAssignDeliveryQueue
    {
        ValueTask<AssignDeliveryContext> DequeueAsync(CancellationToken cancellationToken);
        ValueTask EnqueueAsync(AssignDeliveryContext assignDeliveryContext);

    }
}
