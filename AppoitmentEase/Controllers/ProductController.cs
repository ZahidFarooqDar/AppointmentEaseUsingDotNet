using AutoMapper;
using Azure.Core;
using EcommereAPI.Data;
using EcommereAPI.DomainModels;
using EcommereAPI.Helpers;
using EcommereAPI.ServiceModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EcommereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProjectEcommerceContext _context;
        private readonly IMapper _mapper;

        public ProductController(ProjectEcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.ProductDM
         .Include(p => p.ProductCategories)
         .ToListAsync();

            var productModels = products.Select(p => new ProductDM
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Description = p.Description,
                StockQuantity = p.StockQuantity,
                Price = p.Price,
                ProductCategories = p.ProductCategories.Select(pc => new ProductCategoryDM
                {
                    CategoryId = pc.CategoryId,
                    CategoryName = pc.CategoryName
                }).ToList()
            }).ToList();

            return Ok(productModels);
        }

        // GET: api/Product/5
        [HttpGet("byId")]
        public async Task<IActionResult> GetProduct([FromQuery] int productId)
        {
            var product = await _context.ProductDM
                .Include (p => p.ProductCategories)
                .SingleOrDefaultAsync(p => p.ProductId == productId);
            if (product == null)
            {
                return NotFound();
            }

            var productModel = _mapper.Map<ProductSM>(product);
            return Ok(productModel);
        }

        #region Demo JSON for POST Product with new Category
        /* {
                "productName": "Sample Product",
                    "description": "This is a sample product description.",
                    "stockQuantity": 10,
                    "price": 29.99,
                    "productCategories": [
                    {
                    "categoryName": "Category A",
                        "products": []
                        },
                    {
                    "categoryName": "Category B",
                        "products": []
                        }
                ]
            }*/
        #endregion
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productModel)
        {
            var _product = _mapper.Map<ProductDM>(productModel);
            _context.ProductDM.Add(_product);
            _context.SaveChanges();
            return CreatedAtAction("GetProduct",null, productModel);
        }
        #region Demo JSON for post Product with Existing Category
        /* {

               "productName": "Xiomi5",
               "description": "Longer Battery Life",
               "stockQuantity": 10,
               "price": 20000,
               "productCategories":
                [
                   {
                   "categoryId": 1
                   }
                ]
               }*/
        #endregion
        [HttpPost("WithExistingCategory")]
        public async Task<IActionResult> CreateProductWithExistingCategory([FromBody] ProductSM productModel)
        { 
            var existingCategory = await _context.ProductCategories.FindAsync(productModel.ProductCategories.First().CategoryId);
            if (existingCategory == null)
            {
                // Handle the case where the category does not exist, return an error or perform other actions.
                return BadRequest("The specified product category does not exist.");
            }
            var newProduct = new ProductDM
            {
                ProductName = productModel.ProductName,
                Description = productModel.Description,
                Price = productModel.Price,
                StockQuantity = productModel.StockQuantity,
                ProductCategories = new List<ProductCategoryDM> {existingCategory}
                
            };

            _context.ProductDM.Add(newProduct);
            await _context.SaveChangesAsync();
            //var product = _mapper.Map<ProductDM>(productModel);
            return CreatedAtAction("GetProduct", new { id = productModel.ProductId }, newProduct);
            
        }
        [HttpPost("AssociateProductWithCategory")]
        public async Task<IActionResult> AssociateProductWithCategory([FromQuery] int ProductId, [FromQuery] int CategoryId)
        {
           
            var product = await _context.ProductDM
           .Include(c=> c.ProductCategories).FirstOrDefaultAsync(p => p.ProductId == ProductId);
            var category = await _context.ProductCategories.FindAsync(CategoryId);
            if (product == null || category == null)
            {
                return NotFound($"Product with ID {ProductId}  or Category with ID: {CategoryId} is not found....");
            }
            if (product.ProductCategories.Any(c=> c.CategoryId == CategoryId))
            {
                return BadRequest("The Product is already assigned to the Category.");
            }
            product.ProductCategories.Add(category);
            await _context.SaveChangesAsync();
            return Ok($"Product with ID {ProductId} associated with Category ID {CategoryId} successfully.");
        }



        #region Demo JSON for Adding List of Products
        /*          //DEMO JSON
                  [
          {
                      "productName": "P1",
              "description": "D1",
              "stockQuantity": 170,
              "price": 2000.0,
              "productCategories": [
                  {
                          "categoryId": 1
                  }
              ]
          },
          {
                      "productName": "P2",
              "description": "D2",
              "stockQuantity": 1870,
              "price": 200.00,
              "productCategories": [
                  {
                          "categoryId": 2
                  }
              ]
          }
      ]*/
        #endregion JSON for Adding List of Products
        [HttpPost("list")]
        public async Task<IActionResult> CreateListProducts([FromBody] List<ProductDTO> productModels)
        {
            // Create a list to store the newly created products
            List<ProductDM> newProducts = new List<ProductDM>();

            foreach (var productModel in productModels)
            {
               /* var existingCategory = await _context.ProductCategories.FindAsync(productModel.ProductCategories.First().CategoryId);
                if (existingCategory == null)
                {
                    // Handle the case where the category does not exist, return an error or perform other actions.
                    return BadRequest("The specified product category does not exist.");
                }*/

                var newProduct = new ProductDM
                {
                    ProductName = productModel.ProductName,
                    Description = productModel.Description,
                    Price = productModel.Price,
                    StockQuantity = productModel.StockQuantity
                    //ProductCategories = new List<ProductCategoryDM> { existingCategory }
                };

                _context.ProductDM.Add(newProduct);
                newProducts.Add(newProduct); // Add the newly created product to the list
            }

            await _context.SaveChangesAsync();

            // Return the newly created products
            return CreatedAtAction("GetProducts", newProducts);
            //Problem here is infinite looping 

        }

        #region Demo JSON for update Product
        /*{
          "productName": "CSE Depth",
          "description": "Depth Knowledge of CSE",
          "stockQuantity": 5,
          "price": 50.99
        }*/
    #endregion
    [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductDTO productModel)
        {
            // Check if the product with the given id exists in the database
            var existingProduct = await _context.ProductDM.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound(); // Product not found
            }

            // Update only the specific properties from productModel
            existingProduct.ProductName = productModel.ProductName;
            existingProduct.Description = productModel.Description;
            existingProduct.Price = productModel.Price;
            existingProduct.StockQuantity = productModel.StockQuantity;
            // Add other properties that you want to update

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(existingProduct);
        }




        // This will delete the Product without effecting Category
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {
            var product = await _context.ProductDM.FindAsync(productId);
            if (product == null)
            {
                return NotFound($"Product with ID: {productId} is not Available Try Another Id...");
            }

            _context.ProductDM.Remove(product);
            await _context.SaveChangesAsync();

            return Ok($"Product with ID: {productId} deleted Sucessfully...");
        }
    }
}
