using DDD.Domain.Models;

namespace FoodDelivery.Delivering.Domain.AgregationModels.CourierDeliveryAgregate
{
    public class CourierDelivery : Entity
    {
        public long CourierId { get; }
        public long DeliveryId { get; }
        public CourierDeliveryStatus Status{ get; }
        public DateTime AssignDateTime { get; }
    }

    public class CourierDeliveryStatus : Enumeration
    {
        public static CourierDeliveryStatus WaitingConfirm = new CourierDeliveryStatus(1,nameof(WaitingConfirm));
        public static CourierDeliveryStatus InProgress  = new CourierDeliveryStatus(2,nameof(InProgress));
        public static CourierDeliveryStatus Delivered  = new CourierDeliveryStatus(3,nameof(Delivered));
        public static CourierDeliveryStatus Miss  = new CourierDeliveryStatus(4,nameof(Miss));
        public static CourierDeliveryStatus Canceled  = new CourierDeliveryStatus(5,nameof(Canceled));

        public CourierDeliveryStatus(int id, string name) : base(id, name)
        {
        }
    }
}
