using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;

namespace FoodDelivery.Delivering.API.Application.Services
{
    public class AssignDeliveryContext
    {
        public AssignDeliveryContext(Delivery delivery) 
        { 
            Delivery = delivery;
        }

        public AssignDeliveryContext(Delivery delivery, long? courierId)
        {
            Delivery = delivery;
            CourierId = courierId;
        }

        public Delivery Delivery { get; set; }
        public long? CourierId { get; set; }
    }
}
