using DDD.Domain.Exeption;
using DDD.Domain.Models;
using FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects;
using FoodDelivery.Delivering.Domain.Events;

namespace FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate
{
    public class Delivery : Entity
    {
        private Delivery()
        {
        }

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
            
            AddDomainEvent(new DeliveryCreatedDomainEvent(this));
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
        
        public DateTime? StartDeliveryDateTime { get; private set; }
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
            if(DeliveryStatus != DeliveryStatus.Created)
            {
                StatusChangeException(DeliveryStatus.Assigned);
            }
            CourierId = courierId;
            DeliveryStatus = DeliveryStatus.Assigned;
            StartDeliveryDateTime = DateTime.UtcNow;
            AddDomainEvent(new CourierAssignedDomainEvent(this));
        }

        public void SetWaitingReceiveStatus()
        {
            if (DeliveryStatus != DeliveryStatus.Assigned)
            {
                StatusChangeException(DeliveryStatus.WaitingReceive);
            }
            DeliveryStatus = DeliveryStatus.WaitingReceive;
            AddDomainEvent(new DeliveryStatusChangedToWaitingReceiveDomainEvent(Id));
        }

        public void SetAcceptedForDeliveryStatus()
        {
            if (DeliveryStatus != DeliveryStatus.WaitingReceive)
            {
                StatusChangeException(DeliveryStatus.AcceptedForDelivery);
            }
            DeliveryStatus = DeliveryStatus.AcceptedForDelivery;
            AddDomainEvent(new DeliveryStatusChangedToAcceptedForDeliveryDomainEvent(Id));
        }

        public void SetArrivedAtDeliveryLocationStatus()
        {
            if (DeliveryStatus != DeliveryStatus.AcceptedForDelivery)
            {
                StatusChangeException(DeliveryStatus.ArrivedAtDeliveryLocation);
            }
            DeliveryStatus = DeliveryStatus.ArrivedAtDeliveryLocation;
            AddDomainEvent(new DeliveryStatusChangedToArrivedAtDeliveryLocationDomainEvent(Id));
        }
        public void SetDeliveredStatus()
        {
            if (DeliveryStatus != DeliveryStatus.ArrivedAtDeliveryLocation)
            {
                StatusChangeException(DeliveryStatus.Delivered);
            }
            DeliveryStatus = DeliveryStatus.Delivered;
            DeliveredAt = DateTime.UtcNow;
            AddDomainEvent(new DeliveryStatusChangedToDeliveredDomainEvent(this));
        }

        public void SetCanceledStatus()
        {
            if (DeliveryStatus == DeliveryStatus.Delivered)
            {
                StatusChangeException(DeliveryStatus.Canceled);
            }
            DeliveryStatus = DeliveryStatus.Canceled;
            AddDomainEvent(new DeliveryStatusChangedToCanceledDomainEvent(this));
        }
        private void StatusChangeException(DeliveryStatus deliveryStatusToChange)
        {
            throw new DomainExeption($"Is not possible to change the delivery status from {DeliveryStatus} to {deliveryStatusToChange}.");
        }

        #endregion
    }
}
