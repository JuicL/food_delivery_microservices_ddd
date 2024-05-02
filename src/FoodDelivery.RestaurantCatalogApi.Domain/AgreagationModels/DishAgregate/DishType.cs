using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate
{
    public class DishType : Enumeration
    {
        public static DishType Starters = new(1, nameof(Starters));
        public static DishType Soups = new(2, nameof(Soups));
        public static DishType Salads = new(3, nameof(Salads));
        public static DishType MainDishes = new (4, nameof(MainDishes));
        public static DishType Snacks = new(5, nameof(Snacks));
        public static DishType Sauces = new(6, nameof(Sauces));
        public static DishType Desserts = new(7, nameof(Desserts));
        public static DishType SoftDrinks  = new (8, nameof(SoftDrinks));
        public static DishType HotDrinks  = new (9, nameof(HotDrinks));
        public static DishType Sides = new (10, nameof(Sides));

        private DishType() { }
        public DishType(int id, string name) : base(id, name)
        {
        }

    }
}