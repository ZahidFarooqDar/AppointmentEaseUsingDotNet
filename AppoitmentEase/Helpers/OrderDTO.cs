namespace EcommereAPI.Helpers
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public OrderStatus Status { get; set; }
    }
}
