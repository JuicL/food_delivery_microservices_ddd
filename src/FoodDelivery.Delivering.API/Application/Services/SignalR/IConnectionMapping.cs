
namespace FoodDelivery.Delivering.API.Application.Services.SignalR
{
    public interface IConnectionMapping<T>
    {
        int Count { get; }

        void Add(T key, string connectionId);
        IEnumerable<string> GetConnections(T key);
        void Remove(T key, string connectionId);
    }
}