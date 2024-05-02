
namespace FoodDelivery.Delivering.API.Application.Services.SignalR
{
    public interface IDeliverySignalRHubService
    {
        Task AssignDeliveryRequestAsync(string courierPhoneNumber, string deliveryId, string senderAddress, string deliveryAddress);
    }
}