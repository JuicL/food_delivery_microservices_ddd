using DDD.Domain.Exeption;
using DDD.Domain.Models;
using NetTopologySuite.Geometries;

namespace FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate
{
    public class Address : ValueObject
    {
        private Address() { }
        public Address(string country, string city, string street, string home)
        {
            Country = country;
            City = city;
            Street = street;
            Home = home;
        }

        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; private set; }
        /// <summary>
        /// Город
        /// </summary>
        public string City { get; private set; }
        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; private set; }
        /// <summary>
        /// Номер строения
        /// </summary>
        public string Home { get; private set; }
       


        public string GetFullAddress()
        {
            return string.Join(",", Country, City, Street, Home);
        }

        public static Address Parse(string address)
        {
            var splitAddress = address.Split(',');
            if (splitAddress.Length < 4)
                throw new DomainExeption("Uncorrect address format");
            return new Address(
                    splitAddress[0],
                    splitAddress[1],
                    splitAddress[2],
                    splitAddress[3]
                    );
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Country;
            yield return City;
            yield return Street;
            yield return Home;
        }
    }
}