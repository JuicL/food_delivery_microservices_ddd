using FoodDelibery.Catalog.API.IntegrationEvents;
using FoodDelibery.IntegrationEventLogEF.Services;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.Infrastructure;
using FoodDelivery.RestaurantCatalogApi.Infrastructure.Extention;
using FoodDelivery.RestaurantCatalogApi.Infrastructure.Repository.Implementation;


var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using(var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<RestaurantCatalogContext>();
    //context.Database.EnsureDeleted();

    context.Database.EnsureCreated();
    DatabaseInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
