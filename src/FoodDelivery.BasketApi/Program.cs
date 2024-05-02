using FoodDelivery.BasketApi.Grpc;
using FoodDelivery.BasketApi.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddStackExchangeRedisCache(options => { options.Configuration = "127.0.0.1:6379"; });

services.AddEndpointsApiExplorer();

builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen();

services.AddSingleton<IBasketRepository, RedisBasketRepository>();

var app = builder.Build();

app.UseSwagger();
if (app.Environment.IsDevelopment())
{ 
    app.UseSwaggerUI();
}


app.MapGrpcService<BasketService>();

app.Run();
