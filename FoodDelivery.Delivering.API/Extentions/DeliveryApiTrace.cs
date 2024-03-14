using FoodDelivery.Delivering.Domain.AgregationModels.DeliveryAgregate;

namespace FoodDelivery.Delivering.Extention;

public static partial class DeliveryApiTrace
{
    [LoggerMessage(EventId = 1, EventName = "DeliveryStatusUpdated", Level = LogLevel.Trace, Message = "Delivery with Id: {deliveryId} has been successfully updated to status {status}")]
    public static partial void LogDeliveryStatusUpdated(ILogger logger, long deliveryId, DeliveryStatus status);

}
