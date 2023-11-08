using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.DomainModels
{
    public class ProductCategoryDM
    {
        [Key]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<ProductDM>? Products { get; set; }
    }
}
