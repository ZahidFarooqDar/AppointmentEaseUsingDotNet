namespace EcommereAPI.ServiceModels
{
    public class OrderDetailSM
    {
        public int OrderDetailId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public PaymentDetailsSM PaymentDetails { get; set; }
        /*public Order Order { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }*/
    }
}
