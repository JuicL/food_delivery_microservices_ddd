using DDD.Domain.Exeption;
using DDD.Domain.Models;
using FoodDelivery.Delivery.Domain.AgregationModels.ValueObjects;
using FoodDelivery.Delivery.Domain.Events;

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
        public void SetLateness(long lateness)
        {
            if(lateness <= 0)
            {
                throw new DomainExeption("Invalid lateness value");
            }
            Lateness = lateness;
        }

        public void AssignCourier(long courierId)
        {
            CourierId = courierId;
            DeliveryStatus = DeliveryStatus.Assigned;
            StartDelivery = DateTime.UtcNow;
            AddDomainEvent(new CourierAssignedDomainEvent(Id));
        }

        public void SetWaitingReceiveStatus()
        {
            DeliveryStatus = DeliveryStatus.WaitingReceive;
            AddDomainEvent(new DeliveryStatusChangedToWaitingReceiveDomainEvent(Id));
        }

        public void SetAcceptedForDeliveryStatus()
        {
            DeliveryStatus = DeliveryStatus.AcceptedForDelivery;
            AddDomainEvent(new DeliveryStatusChangedToAcceptedForDeliveryDomainEvent(Id));
        }

        public void SetArrivedAtDeliveryLocationStatus()
        {
            DeliveryStatus = DeliveryStatus.ArrivedAtDeliveryLocation;
            AddDomainEvent(new DeliveryStatusChangedToDeliveredLocationDomainEvent(Id));
        }
        public void SetDeliveredStatus()
        {
            DeliveryStatus = DeliveryStatus.Delivered;
            DeliveredAt = DateTime.UtcNow;
            AddDomainEvent(new DeliveryStatusChangedToDeliveredDomainEvent(Id));
        }

        public void SetCanceledStatus()
        {
            DeliveryStatus = DeliveryStatus.Canceled;
            AddDomainEvent(new DeliveryStatusChangedToCanceledDomainEvent(Id));
        }

        #endregion
    }
}
