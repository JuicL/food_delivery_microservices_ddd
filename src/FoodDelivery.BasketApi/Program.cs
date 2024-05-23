using FoodDelivery.BasketApi.Grpc;
using FoodDelivery.BasketApi.Repositories;
using FoodDelivery.ServiceDefaults;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog();
var services = builder.Services;

services.AddStackExchangeRedisCache(options => { options.Configuration = "127.0.0.1:6379"; });

services.AddEndpointsApiExplorer();

services.AddGrpc().AddJsonTranscoding();
services.AddGrpcSwagger();
services.AddSwaggerGen();

services.AddSingleton<IBasketRepository, RedisBasketRepository>();

var app = builder.Build();

app.UseSwagger();
if (app.Environment.IsDevelopment())
{ 
    app.UseSwaggerUI();
}


app.MapGrpcService<BasketService>();

app.Run();
