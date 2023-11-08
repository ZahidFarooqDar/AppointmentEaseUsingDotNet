namespace EcommereAPI.Helpers
{
    public class OrderProductRequestDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string? ShipmentAddress { get; set; }
    }
}
