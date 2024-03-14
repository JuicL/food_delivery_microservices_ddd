using FoodDelivery.OrderApi.Domain.AgregationModels.ValueObjects;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;
using System.Xml.Linq;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.UserAgregate
{
    public class User : Entity
    {
        private User() { }
        
        public string UserName { get; set; }
        public Phone Phone { get; }

        public User(long id,string userName,Phone phone)
        {
            this.Id = id;
            UserName = !string.IsNullOrWhiteSpace(userName) ? userName : throw new ArgumentNullException(nameof(userName));
            Phone = phone;
        }
    }
}
