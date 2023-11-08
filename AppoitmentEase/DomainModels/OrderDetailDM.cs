using System.ComponentModel.DataAnnotations;


namespace EcommereAPI.DomainModels
{
    public class OrderDetailDM
    {
        [Key]
        public int OrderDetailsId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}
