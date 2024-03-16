using DDD.Domain.Models;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate;

namespace FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate
{
    public class AssignDelivery : Entity
    {
        private AssignDelivery() { }
        public AssignDelivery(long courierId, long deliveryId)
        {
            CourierId = courierId;
            DeliveryId = deliveryId;
            Status = AssignDeliveryStatus.WaitingConfirm;
            AssignDateTime = DateTime.UtcNow;
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
            }
        }
        public void SetDeliveredStatus()
        {
            if(Status == AssignDeliveryStatus.InProgress)
            {
                Status = AssignDeliveryStatus.Delivered;
            }
        }
        public void SetMissStatus()
        {
            if(Status == AssignDeliveryStatus.WaitingConfirm)
            {
                Status = AssignDeliveryStatus.Miss;
            }
        }
        public void SetCanceledStatus()
        {
            if(Status == AssignDeliveryStatus.InProgress)
            {
                Status = AssignDeliveryStatus.Canceled;
            }
        }
    }
}
