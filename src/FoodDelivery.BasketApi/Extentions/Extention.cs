using FoodDelivery.BasketApi.Repositories;

namespace FoodDelivery.BasketApi.Extentions
{
    public static class Extention
    {
        public static IHostApplicationBuilder AddAplicationServices(this IHostApplicationBuilder builder)
        {
            var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
            if (redisConnectionString is null)
            {
                throw new Exception("Connection string for Redis not exit");
            }
            builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = redisConnectionString; });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddGrpc().AddJsonTranscoding();
            builder.Services.AddGrpcSwagger();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton<IBasketRepository, RedisBasketRepository>();
            return builder;
        }
    }
}
