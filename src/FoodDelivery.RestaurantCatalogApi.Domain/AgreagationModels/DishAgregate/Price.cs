using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.RestaurantCatalogApi.Domain.AgreagationModels.DishAgregate
{
    public class Price : ValueObject
    {
        private Price() { }

        public decimal Amount { get; }

        public Price(decimal amount)
        {
            if(amount <= 0) {
                throw new ArgumentException("Amount of price less or equal zero");
            }

            Amount = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
        }
    }
}