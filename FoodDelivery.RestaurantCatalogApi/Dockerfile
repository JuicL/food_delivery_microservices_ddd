FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["FoodDelivery.RestaurantCatalogApi/FoodDelivery.RestaurantCatalogApi.csproj","FoodDelivery.RestaurantCatalogApi/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["FoodDelivery.RestaurantCatalogApi.Domain/FoodDelivery.RestaurantCatalogApi.Domain.csproj", "FoodDelivery.RestaurantCatalogApi.Domain/"]
COPY ["FoodDelivery.RestaurantCatalogApi.Infrastructure/FoodDelivery.RestaurantCatalogApi.Infrastructure.csproj", "FoodDelivery.RestaurantCatalogApi.Infrastructure/"]
COPY ["FoodDelivery.ServiceDefaults/FoodDelivery.ServiceDefaults.csproj", "FoodDelivery.ServiceDefaults/"]
COPY ["IntegrationEventLogEF/IntegrationEventLogEF.csproj", "IntegrationEventLogEF/"]

RUN dotnet restore "FoodDelivery.RestaurantCatalogApi/FoodDelivery.RestaurantCatalogApi.csproj"
COPY . .
WORKDIR "/src/FoodDelivery.RestaurantCatalogApi"
RUN dotnet build "FoodDelivery.RestaurantCatalogApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FoodDelivery.RestaurantCatalogApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FoodDelivery.RestaurantCatalogApi.dll"]