using EcommereAPI.Helpers;
using EcommereAPI.ServiceModels;
using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.DomainModels
{
    public class OrderDM
    {
        [Key]
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }
        public OrderStatus Status { get; set; }
        public ICollection<OrderDetailDM>? OrderDetails { get; set; }
        public TrackingDetailsDM? TrackingDetails { get; set; }

    }
}
