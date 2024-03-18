
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Infrastructure;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public class AssignedDeliveryService : BackgroundService, IAssignedDeliveryService
    {
        private readonly DeliveryContext _deliveryContext;

        public AssignedDeliveryService(DeliveryContext deliveryContext)
        {
            _deliveryContext = deliveryContext;
        }

        public Task<long> AssignDeliveryToCourier(Delivery delivery)
        {
            throw new NotImplementedException();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
