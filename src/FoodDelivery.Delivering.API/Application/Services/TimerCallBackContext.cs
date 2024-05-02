using FoodDelivery.Delivering.Infrastructure;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public record TimerCallBackContext(DeliveryContext DeliveryContext, AssignDeliveryContext AssignDeliveryContext);
}
