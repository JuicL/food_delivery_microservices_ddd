using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.Delivery.Domain.AgregationModels.DeliveryAgregate
{
    public class DeliveryStatus : Enumeration
    {
        public static DeliveryStatus Created = new DeliveryStatus(1, nameof(Created));
        public static DeliveryStatus Assigned = new DeliveryStatus(2, nameof(Assigned));
        public static DeliveryStatus WaitingReceive = new DeliveryStatus(3, nameof(WaitingReceive));
        public static DeliveryStatus AcceptedForDelivery = new DeliveryStatus(4, nameof(AcceptedForDelivery));
        public static DeliveryStatus ArrivedAtDeliveryLocation = new DeliveryStatus(5, nameof(ArrivedAtDeliveryLocation));
        public static DeliveryStatus Delivered = new DeliveryStatus(6, nameof(Delivered));
        public static DeliveryStatus Canceled = new DeliveryStatus(7, nameof(Canceled));
        public DeliveryStatus(int id, string name) : base(id, name)
        {
        }
    }
}
