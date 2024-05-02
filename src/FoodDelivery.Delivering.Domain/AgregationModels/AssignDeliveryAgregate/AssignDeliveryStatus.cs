using DDD.Domain.Models;

namespace FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate
{
    public class AssignDeliveryStatus : Enumeration
    {
        public static AssignDeliveryStatus WaitingConfirm = new AssignDeliveryStatus(1,nameof(WaitingConfirm));
        public static AssignDeliveryStatus InProgress  = new AssignDeliveryStatus(2,nameof(InProgress));
        public static AssignDeliveryStatus Delivered  = new AssignDeliveryStatus(3,nameof(Delivered));
        public static AssignDeliveryStatus Miss  = new AssignDeliveryStatus(4,nameof(Miss));
        public static AssignDeliveryStatus Canceled  = new AssignDeliveryStatus(5,nameof(Canceled));

        public AssignDeliveryStatus(int id, string name) : base(id, name)
        {
        }
    }
}
