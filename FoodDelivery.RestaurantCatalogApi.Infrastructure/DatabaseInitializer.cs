using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAvaibleAgregate;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.RestaurantCatalogApi.Infrastructure
{
    public static class DatabaseInitializer
    {
        public static  void Initialize(RestaurantCatalogContext db)
        {

            if (!db.DishTypes.AnyAsync().Result)
            {
                db.DishTypes.AddRange(Enumeration.GetAll<DishType>());
                db.SaveChanges();
            }
        }
    }
}
