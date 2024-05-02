using MediatR;

namespace FoodDelivery.Delivering.API.Application.Commands.DeliveryCommands
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
}
