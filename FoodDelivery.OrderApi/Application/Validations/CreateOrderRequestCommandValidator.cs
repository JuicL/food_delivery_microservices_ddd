using FluentValidation;
using FoodDelivery.OrderApi.Application.Commands;
using FoodDelivery.OrderApi.DTOs;

namespace FoodDelivery.OrderApi.Application.Validations
{
    public class CreateOrderRequestCommandValidator : AbstractValidator<CreateOrderRequestCommand>
    {
        public CreateOrderRequestCommandValidator(ILogger<CreateOrderRequestCommandValidator> logger)
        {
            RuleFor(command => command.RestaurantAddress).Must(ContainsCommas).WithMessage("Uncorrect address format");
            RuleFor(command => command.DeliveryAddress).Must(ContainsCommas).WithMessage("Uncorrect address format");
            RuleFor(command => command.PaymentMethod).NotEmpty();
            RuleFor(command => command.Dishes).Must(ContainOrderItems).WithMessage("No order items found");
            RuleFor(command => command.Dishes).Must(ContainOrderItems).WithMessage("No order items found");
            RuleFor(command => command.OrderTime).Must(TimeInPast).WithMessage("Invalid time");

        }
        private bool ContainOrderItems(IEnumerable<DishesDTO> orderItems)
        {
            return orderItems.Any();
        }
        private bool ContainsCommas(string address)
        {
            return address.Split(',').Count() > 4;
        }
        private bool TimeInPast(DateTime time)
        {
            return time <= DateTime.UtcNow;
        }
    }
}
