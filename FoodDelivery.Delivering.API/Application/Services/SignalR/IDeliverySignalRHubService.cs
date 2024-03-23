using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.Delivering.API.Application.Services.SignalR
{
    public interface IDeliverySignalRHubService
    {

    }

    public class DeliverySignalRHubService : IDeliverySignalRHubService
    {
        public Dictionary<string, List<string>> Connection = new();

        private readonly IHubContext<DeliveryHub> _hubContext;

        public DeliverySignalRHubService(IHubContext<DeliveryHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task AssignDeliveryRequestAsync(string courierId, string deliveryId)
        {
            await _hubContext.Clients.User(courierId).SendAsync("AssignDeliveryRequest", deliveryId);
        }

        //CourierLocationUpdated

        //JoinToGroup
        
        //LeaveToGroup
        
    }
}
