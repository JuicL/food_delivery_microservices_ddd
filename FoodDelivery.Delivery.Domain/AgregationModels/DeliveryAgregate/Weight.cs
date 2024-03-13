using DDD.Domain.Exeption;
using DDD.Domain.Models;

namespace FoodDelivery.Delivery.Domain.AgregationModels.DeliveryAgregate
{
    public class Weight : ValueObject
    {
        public long Grams { get; }

        public Weight(long grams)
        {
            if (grams <= 0) throw new DomainExeption("Grams count less or equal zero");
            Grams = grams;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
