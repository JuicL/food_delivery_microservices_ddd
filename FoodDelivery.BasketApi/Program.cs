using FoodDelivery.BasketApi.Grpc;
using FoodDelivery.BasketApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

builder.AddRedis("redis");
builder.Services.AddSingleton<IBasketRepository, RedisBasketRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
services.AddGrpc();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGrpcService<BasketService>();

app.Run();
