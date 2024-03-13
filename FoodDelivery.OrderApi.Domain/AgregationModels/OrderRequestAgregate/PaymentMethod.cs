using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate
{
    public class PaymentMethod : Enumeration
    {

        public static PaymentMethod Card = new(1, nameof(Card));
        public static PaymentMethod Cash = new(2, nameof(Cash));
        private PaymentMethod()
        {
        }

        public PaymentMethod(int id, string name) : base(id, name)
        {
        }
    }
}