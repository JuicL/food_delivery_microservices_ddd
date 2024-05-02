using DDD.Domain.Exeption;
using DDD.Domain.Models;
using FoodDelivery.Delivering.Domain.AgregationModels.AssignDeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects;
using NetTopologySuite.Geometries;

namespace FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate
{
    public class Courier: 
        Entity
    {
        private Courier() { }   
        public Courier(long userId, string userName, Phone phoneNumber, WorkAddress workAddress)
        {
            Id = userId;
            UserName = !string.IsNullOrWhiteSpace(userName) ? userName : throw new ArgumentNullException(nameof(userName));
            PhoneNumber = phoneNumber;
            WorkAddress = workAddress;
            WorkStatus = WorkStatus.WorkOff;
        }

        public string UserName { get; }
        public Phone PhoneNumber { get; }
        public WorkAddress WorkAddress { get; private set; }
        public WorkStatus WorkStatus { get; private set; }
       
        public void SetAtWorkStatus(WorkAddress workAddress)
        {
            WorkAddress = workAddress;
            WorkStatus = WorkStatus.AtWork;
        }
        public void SetWorkOffStatus()
        {
            if (WorkStatus == WorkStatus.OnDelivery)
                throw new DomainExeption("Сan't finish the job without completing the delivery");
            WorkStatus = WorkStatus.WorkOff;
        }
        public void SetOnDeliveryStatus()
        {
            if (WorkStatus == WorkStatus.WorkOff)
                throw new DomainExeption("Can't start delivery without starting your work shift");
            WorkStatus = WorkStatus.OnDelivery;
        }

    }

    public class WorkStatus : Enumeration
    {
        public static WorkStatus AtWork = new WorkStatus(1, nameof(AtWork));
        public static WorkStatus WorkOff = new WorkStatus(2, nameof(WorkOff));
        public static WorkStatus OnDelivery = new WorkStatus(3, nameof(OnDelivery));
        public WorkStatus(int id, string name) : base(id, name)
        {
        }
    }
}
