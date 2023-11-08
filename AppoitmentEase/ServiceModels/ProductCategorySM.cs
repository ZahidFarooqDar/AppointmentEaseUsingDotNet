using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.ServiceModels
{
    public class ProductCategorySM
    {
        
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public virtual List<ProductSM>? Products { get; set; }
    }
}
