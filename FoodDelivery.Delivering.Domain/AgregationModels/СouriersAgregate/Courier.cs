using DDD.Domain.Models;
using FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects;
using NetTopologySuite.Geometries;

namespace FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate
{
    public class Courier: 
        Entity
    {
        public Courier(long userId, string userName, Phone phoneNumber, Point location)
        {
            Id = userId;
            UserName = !string.IsNullOrWhiteSpace(userName) ? userName : throw new ArgumentNullException(nameof(userName));
            PhoneNumber = phoneNumber;
            Location = location;
        }

        public string UserName { get; }
        public Phone PhoneNumber { get; }
        public Point Location { get; }
        public WorkStatus WorkStatus { get; }
    
    }

    public class WorkStatus : Enumeration
    {
        public static WorkStatus AtWork = new WorkStatus(1, nameof(AtWork));
        public static WorkStatus OffWork = new WorkStatus(2, nameof(OffWork));
        public static WorkStatus OnDelivery = new WorkStatus(3, nameof(OnDelivery));
        public WorkStatus(int id, string name) : base(id, name)
        {
        }
    }
}
