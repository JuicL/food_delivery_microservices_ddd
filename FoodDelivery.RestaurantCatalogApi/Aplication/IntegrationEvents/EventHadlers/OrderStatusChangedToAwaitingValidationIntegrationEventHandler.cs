using FoodDelibery.Catalog.API.IntegrationEvents;
using FoodDelibery.EventBus.Abstractions;
using FoodDelibery.EventBus.Events;
using FoodDelivery.OrderApi.Application.IntegrationEvents.Events;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.DTOs;
using MediatR;

namespace FoodDelivery.OrderApi.Application.IntegrationEvents.EventHandlers
{
    
    public class OrderStatusChangedToAwaitingValidationIntegrationEventHandler(
    IMediator mediator,
    IBranchRepository branchRepository,
    ICatalogIntegrationEventService catalogIntegrationEvent,
    ILogger<OrderStatusChangedToAwaitingValidationIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>
    {
        public async Task Handle(OrderStatusChangedToAwaitingValidationIntegrationEvent @event)
        {
            var branch = await branchRepository.FindByIdAsync(@event.BranchId,new CancellationTokenSource().Token);
            if (branch is null)
                throw new Exception("Branch not found");
            var response = new OrderConfirmedResponseDTO() { OrderId = @event.OrderId};
            var notFound = false;
            foreach (var dishid in @event.DishesId) 
            {
                var dish = branch.Dishes.FirstOrDefault(x => x.Id == dishid);
                if(dish is null)
                {
                    notFound = true;
                    break;
                }
                response.Dishes.Add(new DishAvaibilityDTO() { DishId = dishid, IsAvaible = dish.IsAvaible });
            }

            var outStock = response.Dishes.Where(x=> !x.IsAvaible).ToList();
            IntegrationEvent integrationEvent = outStock.Any() || notFound ? new OrderStockRejectedIntegrationEvent(@event.OrderId, outStock) : 
                new OrderAvailabilityConfirmedIntegrationEvent(@event.OrderId);
            
            await catalogIntegrationEvent.SaveEventAndCatalogContextChangesAsync(integrationEvent);
            await catalogIntegrationEvent.PublishThroughEventBusAsync(integrationEvent);
        }
    }
    
}
