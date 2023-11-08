using EcommereAPI.DomainModels;

namespace EcommereAPI.ServiceModels
{
    public class TrackingDetailsSM
    {
        public int TrackingDetailsId { get; set; }
        public string? CourierName { get; set; }
        public string? CourierContact { get; set; }
        public bool IsOrderDelivered { get; set; }
        public OrderDM? Order { get; set; }
        /* public int OrderId { get; set; }
         public Order Order { get; set; }*/
    }
}
