namespace FoodDelivery.Delivering.API.DTOs
{
    public record DeliveryResponceDTO(
        long Id,
        long OrderId,
        long? CourierId,
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
        );
}
