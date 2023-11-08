namespace EcommereAPI.Helpers
{
    public class OrderDetailsDTO
    {
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
       // public string? PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
