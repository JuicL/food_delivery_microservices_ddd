using FoodDelivery.IntegrationEventLogEF.Services;
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
using FoodDelivery.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining(typeof(Program));
    cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
    cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
});

services.AddSingleton<IValidator<CreateOrderRequestCommand>, CreateOrderRequestCommandValidator>();

services.AddDbContext<OrderingContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderingApiDatabase"),
        b => b.MigrationsAssembly("FoodDelivery.OrderApi.Infrastructure"));
});


services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<OrderingContext>>();
services.AddTransient<IOrderIntegrationEventService, OrderIntegrationEventService>();

services.AddScoped<IOrderRequestRepository, OrderRequestRepository>();
services.AddScoped<IUserRepository, UserRepository>();

builder.AddRabbitMqEventBus("EventBus")
       .AddEventBusSubscriptions();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
