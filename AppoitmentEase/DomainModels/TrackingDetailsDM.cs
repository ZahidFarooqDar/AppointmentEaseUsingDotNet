using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.DomainModels
{
    public class TrackingDetailsDM
    {
        [Key]
        public int TrackingDetailsId { get; set; }
        public OrderDM? Order { get; set; }
        public int OrderId { get; set; }
        public string? TrackingNumber { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public DateTime DeliveredDate { get; set; }
    }
}
