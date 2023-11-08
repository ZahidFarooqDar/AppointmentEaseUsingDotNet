using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommereAPI.DomainModels
{
    public class UserProductOrderDM
    {
        [Key]
        public int UserProductOrderId { get; set; }
        public int UserId { get; set; }
        public UserDM? User { get; set; }
        
        public int ProductId { get; set; }
        public ProductDM? Product { get; set; }
        public int OrderId { get; set; }
        public OrderDM? Order { get; set; }
    }
}
