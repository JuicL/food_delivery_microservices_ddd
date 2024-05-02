using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;


namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate
{
    public class Branch : Entity
    {
        #region Constructor
        private Branch() { }

        public Branch(Restaurant restaurant, Address address,WorkingHours workingHours, 
            bool isAvailable, 
            List<DishAvaible> dishAvaibles)
        {
            Restaurant = restaurant;
            Address = address;
            WorkingHours = workingHours;
            IsAvailable = isAvailable;
            _dishes = dishAvaibles;
        }

        #endregion

        #region Property

        public Restaurant Restaurant { get; }
        public Address Address { get; private set; }
        public bool IsAvailable { get; private set; }
        public WorkingHours WorkingHours { get; private set; }
        public IReadOnlyCollection<DishAvaible> Dishes => _dishes;

        private readonly List<DishAvaible> _dishes;

        #endregion

        #region Methods
        public void AddDishes(Dish dish, bool isAvaible = false)
        {
            if(_dishes.Any(x=> x.Dish == dish))
            {
                throw new Exception("");
            }
            var dishAvaible = new DishAvaible(dish, isAvaible);
            _dishes.Add(dishAvaible);
        }
        public void RemoveDishes(Dish dish)
        {
            var removeDish = _dishes.FirstOrDefault(x => x.Dish == dish);
            if(removeDish is null)
            {
                throw new Exception("");
            }
            _dishes.Remove(removeDish);
        }
        public void ChangeAddress(Address address)
        {
            Address = address;
        }
        public void ChangeWorkingHours(WorkingHours workingHours)
        {
            WorkingHours = workingHours;
        }
        public void ChangeAvaibleStatus(bool newState)
        {
            IsAvailable = newState;
        }

        #endregion
    }
}
