using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.Delivering.API.Application.Services.SignalR
{
    [Authorize]
    public class DeliveryHub : Hub
    {
        private readonly IConnectionMapping<string> _connections;
        private readonly IMediator _mediator;

        public DeliveryHub(IConnectionMapping<string> connections,IMediator mediator)
        {
            _connections = connections;
            _mediator = mediator;
        }
        public override async Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;
            _connections.Add(userId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string userId = Context.UserIdentifier;
            _connections.Remove(userId, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

        public async Task JoinToGroupAsync(string groupId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task LeaveFromGroup(string groupId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupId);
        }

        public async Task UpdateCourierLocationAsync(string groupId, string location)
        {
            string userId = Context.UserIdentifier;
            await Clients.GroupExcept(groupId, _connections.GetConnections(userId)).SendAsync("CourierLocationUpdated", location);
        }

       
    }
}
