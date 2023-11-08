using System.ComponentModel.DataAnnotations;

namespace EcommereAPI.DomainModels
{
    public class ProductDM
    {
        [Key]
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; } // Brand or manufacturer
        public string Category { get; set; } // Product category
        public string Subcategory { get; set; } // Subcategory within the category
        //public string SKU { get; set; } // Stock Keeping Unit (unique identifier)
       
        public string Color { get; set; } // Color options
        public string Size { get; set; } // Size options
        public bool IsAvailable { get; set; } // Indicates product availability
        public DateTime ReleaseDate { get; set; } // Product release date
        public DateTime LastModified { get; set; } // Last modification date
        public string ManufacturerPartNumber { get; set; } // Manufacturer's part number
        public string EAN { get; set; } // European Article Number (barcode)
        public string UPC { get; set; } // Universal Product Code (barcode)
        //public List<string> Features { get; set; } // List of product features
        //public List<string> Tags { get; set; } // Tags or keywords associated with the product
        //public List<Review> Reviews { get; set; }

        public ICollection<ProductCategoryDM>? ProductCategories { get; set; }
        //public ICollection<UserProductOrderDM>? UserProductOrders { get; set; }




        /*public int ProductId { get; set; } // Unique identifier for the product
        public string Name { get; set; } // Product name
        public string Description { get; set; } // Product description
        public decimal Price { get; set; } // Product price
        public string Brand { get; set; } // Brand or manufacturer
        public string Category { get; set; } // Product category
        public string Subcategory { get; set; } // Subcategory within the category
        public string SKU { get; set; } // Stock Keeping Unit (unique identifier)
        public int StockQuantity { get; set; } // Available stock quantity
        public string Color { get; set; } // Color options
        public string Size { get; set; } // Size options
        public bool IsAvailable { get; set; } // Indicates product availability
        public DateTime ReleaseDate { get; set; } // Product release date
        public DateTime LastModified { get; set; } // Last modification date
        public string ManufacturerPartNumber { get; set; } // Manufacturer's part number
        public string EAN { get; set; } // European Article Number (barcode)
        public string UPC { get; set; } // Universal Product Code (barcode)
        public string ImageUrl { get; set; } // URL to the product image
        public List<string> Features { get; set; } // List of product features
        public List<string> Tags { get; set; } // Tags or keywords associated with the product
        public List<Review> Reviews { get; set; }*/

        //hawa k jonkay aaj mosamu sai ruth gyai, 
        //gulu ki shokhiyan jo bawrai aakay lut gyai.
        //badal rahi hay aaj mosamu ki chal zara, 
        //isi bahanai ku na mai  b dil ka haal zara 
        //sawar lu ,, hy sawar lu.....
        //public int SellerId { get; set; }
        //public Seller Seller { get; set; }

        //public List<OrderDetail> OrderDetails { get; set; }
    }
}
