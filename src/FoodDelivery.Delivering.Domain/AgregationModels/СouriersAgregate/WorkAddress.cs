using DDD.Domain.Exeption;
using DDD.Domain.Models;
using System.Diagnostics.Metrics;

namespace FoodDelivery.Delivering.Domain.AgregationModels.СouriersAgregate
{
    public class WorkAddress : ValueObject
    {
        private WorkAddress() { }
        public WorkAddress(string country, string city, string? district = null)
        {
            Country = country;
            City = city;
            District = district;
        }
  
        public string Country { get; private set; }
  
        public string City { get; private set; }
        public string? District { get; private set; }
        public static WorkAddress Parse(string address)
        {
            var splitAddress = address.Split(',');
            if (splitAddress.Length < 2)
                throw new DomainExeption("Uncorrect address format");
            return new WorkAddress(
                    splitAddress[0],
                    splitAddress[1],
                    splitAddress.Length > 2 ? splitAddress[2] : null
                    );
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}