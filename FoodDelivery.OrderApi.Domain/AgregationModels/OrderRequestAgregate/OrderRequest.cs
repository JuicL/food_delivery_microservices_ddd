using DDD.Domain.Exeption;
using FoodDelivery.OrderApi.Domain.AgregationModels.ValueObjects;
using FoodDelivery.OrderApi.Domain.Events;
using FoodDelivery.RestaurantCatalogApi.Domain.Models;

namespace FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate
{
    public class OrderRequest : Entity
    {
        private OrderRequest() { }
        public OrderRequest(int userId,
            string userName,
            Phone phone,
            Address deliveryAddress,
            int branchId,
            Address restaurantAddress,
            PaymentMethod paymentMethod,
            DateTime orderTime,
            string restaurantName,
            List<Dishes> dishes,
            string description)
        {
            UserId = userId;
            UserName = userName;
            Phone = phone;
            DeliveryAddress = deliveryAddress;
            BranchId = branchId;
            RestaurantAddress = restaurantAddress;
            PaymentMethod = paymentMethod;
            OrderStatus = OrderStatus.Created;
            OrderTime = orderTime;
            RestaurantName = restaurantName;
            _dishes = dishes;
            Description = description;
            AddDomainEvent(new OrderCreatedDomainEvent(this, userId, userName, phone));
        }

        public int UserId { get; }
        public string UserName { get; }
        public Phone Phone { get; }
        public Address DeliveryAddress { get; }
        public string RestaurantName { get; }
        public string Description { get; }
        public int BranchId { get; }
        public Address RestaurantAddress { get; }
        public PaymentMethod PaymentMethod { get; }
        public OrderStatus OrderStatus { get; private set; }
        public DateTime OrderTime { get; }
        public IReadOnlyCollection<Dishes> Dishes => _dishes;

        private List<Dishes> _dishes;

        public void AddDishes(Dishes dishes)
        {
            _dishes.Add(dishes);
        }

        public void SetAwaitingValidationStatus()
        {
            if (OrderStatus != OrderStatus.Created)
            {
                StatusChangeException(OrderStatus.AwaitingValidation);
            }
            OrderStatus = OrderStatus.AwaitingValidation;
            AddDomainEvent(new OrderStatusChangedToAwaitingValidationDomainEvent(Id, _dishes));
        }
        public void SetAvailabilityConfirmedStatus()
        {
            if (OrderStatus != OrderStatus.AwaitingValidation)
            {
                StatusChangeException(OrderStatus.AvailabilityConfirmed);
            }
            OrderStatus = OrderStatus.AvailabilityConfirmed;
            AddDomainEvent(new OrderStatusChangedToAvailabilityConfirmedDomainEvent(Id));
        }
        public void SetPaidStatus()
        {
            if (OrderStatus != OrderStatus.AvailabilityConfirmed)
            {
                StatusChangeException(OrderStatus.Paid);
            }
            OrderStatus = OrderStatus.Paid;
            AddDomainEvent(new OrderStatusChangedToPaidDomainEvent(Id));
        }
        public void SetInProcessStatus()
        {
            // Если установлена "Оплата при получении" то не дожидаемся статуса "Оплачено"
            if (OrderStatus != OrderStatus.AvailabilityConfirmed ||
                OrderStatus != OrderStatus.Paid)
            {
                StatusChangeException(OrderStatus.InProcess);
            }
            OrderStatus = OrderStatus.InProcess;
            AddDomainEvent(new OrderStatusChangedToInProcessDomainEvent(Id));
        }
        public void SetDeliveredStatus()
        {
            if (OrderStatus != OrderStatus.InProcess)
            {
                StatusChangeException(OrderStatus.Delivered);
            }
            OrderStatus = OrderStatus.Delivered;
            AddDomainEvent(new OrderStatusChangedToDeliveredDomainEvent(Id));
        }
        public void SetCanceleStatus()
        {
            if (OrderStatus == OrderStatus.Delivered)
            {
                StatusChangeException(OrderStatus.Canceled);
            }
            OrderStatus = OrderStatus.Canceled;
            AddDomainEvent(new OrderStatusChangedToCanceledDomainEvent(Id));
        }
        private void StatusChangeException(OrderStatus orderStatusToChange)
        {
            throw new DomainExeption($"Is not possible to change the order status from {OrderStatus} to {orderStatusToChange}.");
        }
    }
}
