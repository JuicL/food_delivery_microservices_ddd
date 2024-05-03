using FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Extention
{
    public static class Extentions
    {
        public static void AddAplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RestaurantCatalogContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("RestaurantApiDatabase"),
                    b => b.MigrationsAssembly("FoodDelivery.RestaurantCatalogApi.Infrastructure"));
            });
        }

        public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            eventBus.AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
        }
    }
}
