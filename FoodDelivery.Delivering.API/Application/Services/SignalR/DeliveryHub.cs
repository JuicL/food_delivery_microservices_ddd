using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Polly;

namespace FoodDelivery.Delivering.API.Application.Services.SignalR
{

    [Authorize]
    public class DeliveryHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            string userId = Context.UserIdentifier;
          

            return base.OnConnectedAsync();
        }
    }
}
