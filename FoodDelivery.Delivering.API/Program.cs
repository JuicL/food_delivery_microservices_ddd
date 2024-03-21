using FoodDelibery.IntegrationEventLogEF.Services;
using FoodDelivery.Delivering.API.Application.Services;
using FoodDelivery.Delivering.API.Application.Services.SignalR;
using FoodDelivery.Delivering.Application.Behaviors;
using FoodDelivery.Delivering.Application.IntegrationEvents;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.ÑouriersAgregate;
using FoodDelivery.Delivering.Infrastructure;
using FoodDelivery.Delivering.Infrastructure.Repositories.Implementation;
using FoodDelivery.DeliveringAPI.Extentions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddLogging();
services.AddTransient(typeof(ILogger), typeof(Logger<Program>));

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});
//services.AddSingleton<IValidator<CreateOrderRequestCommand>, CreateOrderRequestCommandValidator>();
builder.Services.AddDbContext<DeliveryContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DeliveringApiDatabase"),o =>
        {
            o.MigrationsAssembly("FoodDelivery.DeliveringAPI");
            
        });
});
services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<DeliveryContext>>();
services.AddTransient<IDeliveryIntegrationEventService, DeliveryIntegrationEventService>();

services.AddScoped<IDeliveryRepository, DeliveryRepository>();
services.AddScoped<ICourierRepository, CourierRepository>();
services.AddScoped<IAssignDeliveryRepository, AssignDeliveryRepository>();

services.AddSingleton<AssignDeliveryQueue, AssignDeliveryQueue>();


services.AddSignalR();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();

services.Configure<HostOptions>(x =>{ 
    x.ServicesStartConcurrently = true;
    x.ServicesStopConcurrently = false;
});
services.AddHostedService<AssignedDeliveryService>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<DeliveryContext>();
    //context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHub<DeliveryHub>("/deliveryHub");
app.MapControllers();

app.Run();
