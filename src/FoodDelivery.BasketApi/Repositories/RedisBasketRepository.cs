using FoodDelivery.BasketApi.Model;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace FoodDelivery.BasketApi.Repositories
{
    public class RedisBasketRepository(IDistributedCache cache) : IBasketRepository
    {
        private static RedisKey BasketKeyPrefix = "/basket/"u8.ToArray();
        private static RedisKey GetBasketKey(string userId) => BasketKeyPrefix.Append(userId);
        
        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var data = await cache.GetStringAsync(GetBasketKey(customerId));

            if (data is null || data.Length == 0)
            {
                return null;
            }
            return JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            await cache.RemoveAsync(GetBasketKey(id));
            return true;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var json = JsonSerializer.Serialize(basket);
            await cache.SetStringAsync(GetBasketKey(basket.BuyerId), json);
           
            return await GetBasketAsync(basket.BuyerId);
        }
    }
}