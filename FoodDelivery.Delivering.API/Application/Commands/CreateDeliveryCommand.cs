using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands
{
    public record CreateDeliveryCommand(
        long OrderId,
        long RecipientId,
        string RecipientName,
        string UserPhone,
        long TotalWeight,
        decimal TotalPrice,
        string PaymentMethod,
        string SenderName,
        string SenderAddress,
        string RecipientAddress,
        string Description
        ) : IRequest<bool>;
    public record SetOnWorkCourierStatus(long CourierId) : IRequest<bool>;
    public record SetWorkOffCourierStatus(long CourierId) : IRequest<bool>;
    public record SetCourierStatus(long CourierId) : IRequest<bool>;
}
