using FoodDelivery.Delivery.Domain.AgregationModels.ValueObjects;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.Delivery.Domain.AgregationModels.DeliveryAgregate
{
    public class Delivery : Entity
    {
        public Delivery(long orderId, long recipientId, string recipientName, Phone userPhoneNumber, Weight totalWeight, Price totalPrice, PaymentMethod paymentMethod, string senderName, Address senderAddress, Address recipientAddress, string description)
        {
            OrderId = orderId;
            RecipientId = recipientId;
            RecipientName = recipientName;
            UserPhoneNumber = userPhoneNumber;
            TotalWeight = totalWeight;
            TotalPrice = totalPrice;
            PaymentMethod = paymentMethod;
            SenderName = senderName;
            SenderAddress = senderAddress;
            RecipientAddress = recipientAddress;
            Description = description;
        }

        #region Fields
        
        public long OrderId { get; }
        public long RecipientId { get; }

        public string RecipientName { get; }
        public Phone UserPhoneNumber { get; }

        public Weight TotalWeight { get; }
        public Price TotalPrice { get; }

        public PaymentMethod PaymentMethod { get; }

        public string SenderName { get; }
        public Address SenderAddress { get; }

        public Address RecipientAddress { get; }
        public long? CourierId { get; private set; }
        
        public DateTime? StartDelivery { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        //Minutes
        public long Lateness { get; private set; }
        public DeliveryStatus DeliveryStatus { get; private set; }
        public string Description { get; }

        #endregion

        #region Methods

        public void AssignCourier(long courierId)
        {
            CourierId = courierId;
            DeliveryStatus = DeliveryStatus.Assigned;
            AddDomainEvent(new CourierAssignedDomainEvent(Id));
        }
        public void f()
        {

        }

        public void Foo()
        {

        }
        #endregion
    }
}
