namespace EcommereAPI.Helpers
{
    public class TrackingDetailsDTO
    {
        public int TrackingDetailsId { get; set; }
        public int OrderId { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime DeliveredDate { get; set; }
    }
}
