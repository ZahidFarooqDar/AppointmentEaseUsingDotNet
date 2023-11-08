using EcommereAPI.DomainModels;

namespace EcommereAPI.ServiceModels
{
    public class OrderSM
    {
        public int? OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int? Quantity { get; set; }
        public string? Status { get; set; }
        public decimal? OrderAmount { get; set; }
        public string? PaymentMethod { get; set; }
        public ICollection<UserProductOrderSM>? UserProductOrders { get; set; }
        public TrackingDetailsSM? TrackingDetails { get; set; }

        /*public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public int TrackingDetailsId { get; set; }
        public TrackingDetails TrackingDetails { get; set; }
        public int PaymentDetailsId { get; set; }
        public PaymentDetails PaymentDetails { get; set; }*/
    }
}
