using FoodDelivery.RestaurantCatalogApi.Domain.Models;
using MediatR;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate
{
    public class Weight : ValueObject
    {
        private Weight() { }

        public long Gram { get; private set; }

        public static Weight CreateFromGram(long gram)
        {
            if (gram <= 0)
                throw new ArgumentException("Gram value equal or less zero");
            return new Weight { Gram = gram };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Gram;
        }
    }
}