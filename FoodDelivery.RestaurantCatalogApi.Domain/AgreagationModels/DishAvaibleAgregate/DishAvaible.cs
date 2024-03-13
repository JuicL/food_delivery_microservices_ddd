using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate
{
    public class DishAvaible : Entity
    {
        private DishAvaible() { }
        public DishAvaible(Dish dish, bool isAvaible = false)
        {
            Dish = dish;
            IsAvaible = isAvaible;
        }
        public DishAvaible(Dish dish, Branch branch, bool isAvaible = false)
            : this(dish,isAvaible)
        {
            Branch = branch;
        }
        #region Property

        public Dish Dish { get; }
        public long DishId { get; }
        public Branch Branch { get; }
        public long BranchId { get; }
        public bool IsAvaible { get; private set; }

        #endregion

        #region Methods
        public void ChangeStatus(bool newStatus)
        {
            if (Branch is null)
                throw new Exception("");
            IsAvaible = newStatus;
        }
    
        #endregion

    }
}
