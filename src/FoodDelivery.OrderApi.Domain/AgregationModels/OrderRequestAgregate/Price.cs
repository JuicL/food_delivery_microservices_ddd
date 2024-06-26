﻿using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate
{
    public class Price : ValueObject
    {
        public decimal Amount { get; }

        public Price(decimal amount)
        {
            if(amount <= 0) 
                throw new Exception("Price amount less or equal zero");
            Amount = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}