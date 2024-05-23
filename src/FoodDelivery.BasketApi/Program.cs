using FoodDelivery.BasketApi.Extentions;
using FoodDelivery.BasketApi.Grpc;
using FoodDelivery.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.AddSerilog();
builder.AddAplicationServices();

var app = builder.Build();

app.UseDefaultOpenApi();

app.MapGrpcService<BasketService>();

app.Run();
