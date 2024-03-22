using System.Threading.Channels;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public interface IAssignDeliveryQueue
    {
        ValueTask<AssignDeliveryContext> DequeueAsync(CancellationToken cancellationToken);
        ValueTask EnqueueAsync(AssignDeliveryContext assignDeliveryContext);

    }
    public class AssignDeliveryQueue : IAssignDeliveryQueue
    {
        private readonly Channel<AssignDeliveryContext> _queue;
        public AssignDeliveryQueue()
        {
            _queue = Channel.CreateUnbounded<AssignDeliveryContext>();
        }

        public async ValueTask<AssignDeliveryContext> DequeueAsync(CancellationToken cancellationToken)
        {
            var result = await _queue.Reader.ReadAsync(cancellationToken);
            return result;
        }

        public async ValueTask EnqueueAsync(AssignDeliveryContext assignDeliveryContext)
        {
            await _queue.Writer.WriteAsync(assignDeliveryContext);
        }
    }
}
