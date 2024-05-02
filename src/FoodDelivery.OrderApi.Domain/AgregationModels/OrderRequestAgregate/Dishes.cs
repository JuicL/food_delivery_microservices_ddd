using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate
{
    public class Dishes : Entity
    {
        private Dishes() { }
        public Dishes(int dishId, string name, Weight weight, Price price, int units)
        {
            if (units <= 0)
            {
                throw new Exception("Invalid number of units");
            }

            DishId = dishId;
            Name = name;
            Weight = weight;
            Price = price;
            Units = units;
        }

        public int DishId { get; }
        public string Name { get; }
        public Weight Weight { get; }
        public Price Price { get; }
        public int Units { get; }

    }
}