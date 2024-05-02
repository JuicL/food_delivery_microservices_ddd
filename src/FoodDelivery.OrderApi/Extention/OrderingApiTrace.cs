using FoodDelivery.OrderApi.Domain.AgregationModels.OrderRequestAgregate;

namespace FoodDelivery.OrderApi.Extention;

internal static partial class OrderingApiTrace
{
    [LoggerMessage(EventId = 1, EventName = "OrderStatusUpdated", Level = LogLevel.Trace, Message = "Order with Id: {OrderId} has been successfully updated to status {Status}")]
    public static partial void LogOrderStatusUpdated(ILogger logger, long orderId, OrderStatus status);

}
