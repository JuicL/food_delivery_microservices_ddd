using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate
{
    public class DishAvaibleStatus : Enumeration
    {
        public static DishAvaibleStatus Created = new(1, nameof(Created));
        public static DishAvaibleStatus Avaible = new(2, nameof(Avaible));
        public static DishAvaibleStatus NotAvailable = new (3, nameof(NotAvailable));
        public static DishAvaibleStatus OutOfStock = new (4, nameof(OutOfStock));
        private DishAvaibleStatus() { }

        public DishAvaibleStatus(int id, string name) : base(id, name)
        {
        }
    }
}
