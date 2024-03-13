using FoodDelivery.Delivery.Domain.AgregationModels.ValueObjects;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;
using NetTopologySuite.Geometries;

namespace FoodDelivery.Delivery.Domain.AgregationModels.СouriersAgregate
{
    public class Courier: 
        Entity
    {
        public Courier(long userId, long userName, Phone phoneNumber, Point location)
        {
            UserId = userId;
            UserName = userName;
            PhoneNumber = phoneNumber;
            Location = location;
        }

        public long UserId { get; }
        public long UserName { get; }
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
