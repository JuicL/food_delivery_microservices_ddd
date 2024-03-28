using FoodDelivery.BasketApi.Model;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace FoodDelivery.BasketApi.Repositories
{
    public class RedisBasketRepository(IConnectionMultiplexer redis) : IBasketRepository
    {
        private readonly IDatabase _database = redis.GetDatabase();
        private static RedisKey BasketKeyPrefix = "/basket/"u8.ToArray();
        private static RedisKey GetBasketKey(string userId) => BasketKeyPrefix.Append(userId);

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            using var data = await _database.StringGetLeaseAsync(GetBasketKey(customerId));

            if (data is null || data.Length == 0)
            {
                return null;
            }
            return JsonSerializer.Deserialize<CustomerBasket>(data.Span);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(GetBasketKey(id));
        }


        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var json = JsonSerializer.SerializeToUtf8Bytes(basket);
            var created = await _database.StringSetAsync(GetBasketKey(basket.BuyerId), json);

            if (!created)
            {
                return null;
            }

            return await GetBasketAsync(basket.BuyerId);
        }
    }
}