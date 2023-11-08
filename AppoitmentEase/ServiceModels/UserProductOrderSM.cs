using EcommereAPI.DomainModels;
using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.ServiceModels
{
    public class UserProductOrderSM
    {
       
        public int UserProductOrderId { get; set; }
        public int UserId { get; set; }
        public UserSM? User { get; set; }
        public int ProductId { get; set; }
        public ProductSM? Product { get; set; }
        public int OrderId { get; set; }
        public OrderSM? Order { get; set; }
    }
}
