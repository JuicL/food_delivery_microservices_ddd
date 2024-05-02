using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate
{
    public class Weight : ValueObject
    {
        public long Grams { get; }

        public Weight(long grams)
        {
            if (grams <= 0) throw new Exception("Grams count less or equal zero");
            Grams = grams;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}