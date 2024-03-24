using IdentityModel;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace FoodDelivery.Delivering.API.Application.Services.SignalR
{
    public class UserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext context)
        {
            return ((ClaimsIdentity)context.User.Identity).FindFirst(JwtClaimTypes.PhoneNumber)?.Value;
        }
    }
}
