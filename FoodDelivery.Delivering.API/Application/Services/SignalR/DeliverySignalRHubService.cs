using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.Delivering.API.Application.Services.SignalR
{

    public class DeliverySignalRHubService : IDeliverySignalRHubService
    {
        private readonly IConnectionMapping<string> _connections;

        private readonly IHubContext<DeliveryHub> _hubContext;

        public DeliverySignalRHubService(IHubContext<DeliveryHub> hubContext, IConnectionMapping<string> connections)
        {
            _hubContext = hubContext;
            _connections = connections;
        }

        public async Task AssignDeliveryRequestAsync(string courierPhoneNumber, string deliveryId, string senderAddress, string deliveryAddress)
        {
            var courierConnections = _connections.GetConnections(courierPhoneNumber);
            foreach (var connection in courierConnections)
            {
                await _hubContext.Clients.Client(connection).SendAsync("AssignDeliveryRequest", deliveryId, senderAddress,deliveryAddress);
            }
        }


    }
}
