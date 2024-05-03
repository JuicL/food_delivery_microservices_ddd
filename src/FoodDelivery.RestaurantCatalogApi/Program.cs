using FoodDelivery.Catalog.API.IntegrationEvents;
using FoodDelivery.IntegrationEventLogEF.Services;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.Infrastructure;
using FoodDelivery.RestaurantCatalogApi.Infrastructure.Extention;
using FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation;
using FoodDelivery.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.AddDefaultOpenApi();
builder.AddDefaultAuthentication();

builder.Services.AddControllers();

builder.AddAplicationServices();

builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IDishAvaibleRepository, DishAvaibleRepository>();
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IDishTypeRepository, DishTypeRepository>();
services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<RestaurantCatalogContext>>();
services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();

services.AddMediatR(cfg => 
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
});
services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
var app = builder.Build();

app.UseDefaultOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
