namespace EcommereAPI.Helpers
{
    public class OrderWithProductsRequestDTO
    {
        public int BuyerId { get; set; }
        public List<OrderProductRequestDTO>? Products { get; set; }
    }
}
