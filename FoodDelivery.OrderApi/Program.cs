using FoodDelibery.IntegrationEventLogEF.Services;
using FluentValidation;
using FoodDelivery.OrderApi.Application.Behaviors;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.Application.IntegrationEvents;
using FoodDelivery.OrderApi.Application.Validations;
using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;
using FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate;
using FoodDelivery.OrderApi.Extention;
using FoodDelivery.OrderApi.Infrastructure;
using FoodDelivery.OrderApi.Infrastructure.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddLogging();

// Additional code to register the ILogger as a ILogger<T> where T is the Startup class
services.AddTransient(typeof(ILogger), typeof(Logger<Program>));

services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});
services.AddSingleton<IValidator<CreateOrderRequestCommand>, CreateOrderRequestCommandValidator>();

builder.Services.AddDbContext<OrderingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderingApiDatabase"),
        b => b.MigrationsAssembly("FoodDelivery.OrderApi"));
});


services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<OrderingContext>>();
services.AddTransient<IOrderIntegrationEventService, OrderIntegrationEventService>();

services.AddScoped<IOrderRequestRepository, OrderRequestRepository>();
services.AddTransient<IUserRepository, UserRepository>();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();

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
    var context = serviceProvider.GetRequiredService<OrderingContext>();
    //context.Database.EnsureDeleted();

    context.Database.EnsureCreated();
    
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
