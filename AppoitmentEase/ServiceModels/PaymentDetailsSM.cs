namespace EcommereAPI.ServiceModels
{
    public class PaymentDetailsSM
    {
        public int PaymentDetailsId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public bool PaymentStatus { get; set; }
        public bool IsRefunded { get; set; }
        public OrderSM? OrderSM { get; set; }
        /* public int OrderId { get; set; }
         public Order Order { get; set; }*/
    }
}
