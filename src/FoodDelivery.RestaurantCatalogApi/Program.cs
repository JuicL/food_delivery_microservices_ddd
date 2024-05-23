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
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.AddDefaultOpenApi();
builder.AddDefaultAuthentication();
builder.AddAplicationServices();


//var logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .CreateLogger();
builder.AddSerilog();

services.AddControllers();

services.AddScoped<IRestaurantRepository, RestaurantRepository>();
services.AddScoped<IBranchRepository, BranchRepository>();
services.AddScoped<IDishAvaibleRepository, DishAvaibleRepository>();
services.AddScoped<IDishRepository, DishRepository>();
services.AddScoped<IDishTypeRepository, DishTypeRepository>();
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
