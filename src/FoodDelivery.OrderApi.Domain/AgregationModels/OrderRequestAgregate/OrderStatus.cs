using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate
{
    public class OrderStatus : Enumeration
    {
        public static OrderStatus Created => new OrderStatus(1, nameof(Created));
        //Создание пользователя
        public static OrderStatus AwaitingValidation => new OrderStatus(2, nameof(AwaitingValidation));
        // Проверка доступности
        public static OrderStatus AvailabilityConfirmed => new OrderStatus(3, nameof(AvailabilityConfirmed));
        public static OrderStatus Paid => new OrderStatus(4, nameof(Paid));
        public static OrderStatus InProcess => new OrderStatus(5, nameof(InProcess));
        public static OrderStatus Delivered => new OrderStatus(6, nameof(Delivered));
        public static OrderStatus Canceled => new OrderStatus(7, nameof(Canceled));

        public OrderStatus(int id, string name) : base(id, name)
        {
        }
    }
}