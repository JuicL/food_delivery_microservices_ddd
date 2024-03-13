using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FoodDelivery.RestaurantCatalogApi.Domain.Models
{
    public abstract class Enumeration : IComparable
    {
        public string Name { get; private set; }

        public int Id { get; private set; }
        protected Enumeration() { }

        protected Enumeration(int id, string name) => (Id, Name) = (id, name);

        public override string ToString() => Name;

        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
            typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null))
                .Cast<T>();
        public static bool operator ==(Enumeration obj1, Enumeration obj2)
        {
            return obj1.Equals(obj2);
        }   
        public static bool operator !=(Enumeration obj1, Enumeration obj2) => !(obj1 == obj2);

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

    }
}