using DDD.Domain.Exeption;
using DDD.Domain.Models;
using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;
using System.Text.RegularExpressions;

namespace FoodDelivery.Delivering.Domain.AgregationModels.ValueObjects
{
    public class Phone : ValueObject
    {
        public string Number { get; }

        private Phone(string number)
        {
            Number = number;
        }

        public Phone ParseFromInternational(string number)
        {
            var regex = new Regex("/^\\+((?:9[679]|8[035789]|6[789]|5[90]|42|3[578]|2[1-689])|9[0-58]|8[1246]|6[0-6]|5[1-8]|4[013-9]|3[0-469]|2[70]|7|1)(?:\\W*\\d){0,13}\\d$/gm");
            var result = regex.Match(number);
            if (!result.Success)
            {
                throw new DomainExeption("Invalid phone number");
            }
            return new Phone(number);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
