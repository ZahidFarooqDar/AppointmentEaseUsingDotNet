namespace EcommereAPI.Helpers
{
    public class Ship24TrackingRequest
    {
        public string? TrackingNumber { get; set; }
        public string? ShipmentReference { get; set; }
        public string? OriginCountryCode { get; set; }
        public string? DestinationCountryCode { get; set; }
        public string? DestinationPostCode { get; set; }
        public DateTime ShippingDate { get; set; }
        public List<string>? CourierCode { get; set; }
        public string? CourierName { get; set; }
        public string? TrackingUrl { get; set; }
        public string? OrderNumber { get; set; }
    }
}
