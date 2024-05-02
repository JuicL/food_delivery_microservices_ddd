using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate
{
    public class Dish : Entity
    {
        #region Constructor
        private Dish() { }

        public Dish(string name, Weight weight,Price price, DishType dishType, List<string> ingredients)
        {
            Name = name;
            Ingredients = ingredients;
            Weight = weight;
            Price = price;
            DishType = dishType;
        }
        #endregion

        #region Property
        public string Name { get; private set; }
        public List<string> Ingredients { get; private set; }
        public Weight Weight { get; private set; }
        public DishType DishType { get; private set; }
        public int DishTypeId { get; private set; }
        public Price Price { get; private set; }
        #endregion

        #region Methods
        public void ChangeName(string name)
        {
            Name = name;
        }
        public void ChangeDishType(DishType dishType)
        {
            DishType = dishType;
        }
        public void ChangePrice(Price price)
        {
            Price = price;
        }

        public void ChangeWeight(Weight weight)
        {
            Weight = weight;
        }

        public void ChangeIngredients(List<string> ingredients)
        {
            Ingredients = ingredients;
        }

        public void AddIngredients(string ingredient)
        {
            Ingredients.Add(ingredient);
        }

        #endregion

    }
}
