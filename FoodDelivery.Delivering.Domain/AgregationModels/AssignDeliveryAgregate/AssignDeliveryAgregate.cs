using DDD.Domain.Models;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;
using FoodDelivery.Delivering.Domain.Events;

namespace FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate
{
    public class AssignDelivery : Entity
    {
        private AssignDelivery() { }
        public AssignDelivery(long deliveryId, long courierId)
        {
            CourierId = courierId;
            DeliveryId = deliveryId;
            Status = AssignDeliveryStatus.WaitingConfirm;
            AssignDateTime = DateTime.UtcNow;
            AddDomainEvent(new AssignDeliveryStatusChangedToWaitingConfirmDomainEvent(this));

        }

        public long CourierId { get; }
        public Courier Courier { get; }
        public long DeliveryId { get; }
        public Delivery Delivery { get; }
        public AssignDeliveryStatus Status{ get; private set; }
        public DateTime AssignDateTime { get; }

        public void SetInProcessStatus()
        {
            if(Status == AssignDeliveryStatus.WaitingConfirm)
            {
                Status = AssignDeliveryStatus.InProgress;
                AddDomainEvent(new AssignDeliveryStatusChangedToInProcessDomainEvent(this));
            }
        }
        public void SetDeliveredStatus()
        {
            if(Status == AssignDeliveryStatus.InProgress)
            {
                Status = AssignDeliveryStatus.Delivered;
                AddDomainEvent(new AssignDeliveryStatusChangedToDeliveredDomainEvent(this));

            }
        }
        public void SetMissStatus()
        {
            if(Status == AssignDeliveryStatus.WaitingConfirm)
            {
                Status = AssignDeliveryStatus.Miss;
                AddDomainEvent(new AssignDeliveryStatusChangedToMissDomainEvent(this));

            }
        }
        public void SetCanceledStatus()
        {
            if(Status == AssignDeliveryStatus.InProgress)
            {
                Status = AssignDeliveryStatus.Canceled;
                AddDomainEvent(new AssignDeliveryStatusChangedToCanceledDomainEvent(this));
            }
        }
    }
}
