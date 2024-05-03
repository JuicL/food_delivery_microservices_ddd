﻿namespace FoodDelivery.BasketApi.Model
{
    public class BasketItem
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public long Weigt{ get; set; }
        public int Units { get; set; }
    }
}