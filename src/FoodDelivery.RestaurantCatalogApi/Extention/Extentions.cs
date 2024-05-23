using FoodDelivery.Catalog.API.IntegrationEvents;
using FoodDelivery.IntegrationEventLogEF.Services;
using FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure.Extention
{
    public static class Extentions
    {
        public static IHostApplicationBuilder AddAplicationServices(this IHostApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RestaurantCatalogContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("RestaurantApiDatabase"),
                    b => b.MigrationsAssembly("FoodDelivery.RestaurantCatalogApi.Infrastructure"));
            });
            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();
            builder.Services.AddScoped<IDishAvaibleRepository, DishAvaibleRepository>();
            builder.Services.AddScoped<IDishRepository, DishRepository>();
            builder.Services.AddScoped<IDishTypeRepository, DishTypeRepository>();
            builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<RestaurantCatalogContext>>();
            builder.Services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();
            
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
            });
            return builder;
        }

        public static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            eventBus.AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
        }
    }
}
