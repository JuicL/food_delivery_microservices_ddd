using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.BranchAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.RestaurantAgreagate
{
    public class Restaurant : Entity
    {
        #region Constructor
        private Restaurant() { }
        public Restaurant(string name, List<Branch> branches) {
            Name = name;
            _branches = branches;
        }
        #endregion

        #region Property

        public string Name { get; private set; }
        public IReadOnlyCollection<Branch> Branches => _branches;

        private readonly List<Branch> _branches;

        #endregion

        #region Methods
        public void ChangeName(string name)
        {
            Name = name;
        }

        public void AddBranch(Branch branch)
        {
            _branches.Add(branch);
        }
       
        #endregion
    }
}
