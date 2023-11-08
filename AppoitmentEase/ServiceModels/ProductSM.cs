using EcommereAPI.DomainModels;

namespace EcommereAPI.ServiceModels
{
    public class ProductSM
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public  List<ProductCategorySM>? ProductCategories { get; set; }
        public List<UserProductOrderDM>? UserProductOrders { get; set; }
        //public int SellerId { get; set; }
        /*   public UserSM User { get; set; }
           public List<ProductCategorySM> Categories { get; set; }
           public List<OrderDetailSM> OrderDetails { get; set; }*/
    }
}
