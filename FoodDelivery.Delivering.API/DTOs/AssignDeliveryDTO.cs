namespace FoodDelivery.Delivering.API.DTOs
{
    public class AssignDeliveryDTO
    {
        public long CourierId { get; set; }
        public string Status { get; set; }
        public DeliveryResponceDTO Delivery { get; set; }
    }

}
