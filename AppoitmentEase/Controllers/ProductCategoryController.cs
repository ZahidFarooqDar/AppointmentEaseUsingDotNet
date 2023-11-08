using AutoMapper;
using EcommereAPI.Data;
using EcommereAPI.DomainModels;
using EcommereAPI.Helpers;
using EcommereAPI.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProjectEcommerceContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryController(ProjectEcommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {

            var categories = await _context.ProductCategories.ToListAsync();
            var _categories = categories.Select(c => new ProductCategoryDM
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            }).ToList();
            return Ok(_categories);
        }
        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int CategoryId)
        {
            var category = await _context.ProductCategories.SingleOrDefaultAsync(c =>  c.CategoryId == CategoryId);
            if(category == null)
            {
                return BadRequest("Category Not Available...Try Another ID");
            }
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] ProductCategoryDTO category)
        {
            var existingCategory = await _context.ProductCategories
                .FirstOrDefaultAsync(c => c.CategoryName == category.CategoryName);

            if (existingCategory != null)
            {
                // Category with the same name already exists, return a conflict response
                return Conflict($"Category '{category.CategoryName}' already exists.");
            }

            // Category doesn't exist, so proceed to add it
            var newCategory = _mapper.Map<ProductCategoryDM>(category);
            _context.ProductCategories.Add(newCategory);
            await _context.SaveChangesAsync();

            // Return a success response
            return CreatedAtAction("GetCategories", new { id = newCategory.CategoryId }, category);
        }
        #region Demo JSON for Post Categories
        /*[
            {
                "CategoryName": "Electronics"
            },
            {
                "CategoryName": "Clothing"
            },
            {
            "CategoryName": "Books"
            }
        ]*/
        #endregion
        //Will only post those Categories which are not Already present in Categories
        [HttpPost("AddCategories")]
        public async Task<IActionResult> AddCategories([FromBody] List<ProductCategoryDTO> categories)
        {
            if (categories == null || !categories.Any())
            {
                return BadRequest("No categories provided.");
            }

            // Filter out categories that already exist
            var existingCategoryNames = await _context.ProductCategories
                .Where(ec => categories.Select(c => c.CategoryName).Contains(ec.CategoryName))
                .Select(ec => ec.CategoryName)
                .ToListAsync();

            var newCategories = categories
                .Where(c => !existingCategoryNames.Contains(c.CategoryName))
                .ToList();

            if (!newCategories.Any())
            {
                return Ok("No new categories added, all provided categories already exist.");
            }

            // Map and add the new categories to the context
            foreach (var category in newCategories)
            {
                var newCategory = _mapper.Map<ProductCategoryDM>(category);
                _context.ProductCategories.Add(newCategory);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategories", null, newCategories);
        }


        [HttpPut("{CategoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int CategoryId, [FromBody] ProductCategoryDTO newCategory)
        {
            var ExistingCategory = await _context.ProductCategories.SingleOrDefaultAsync(c => c.CategoryId == CategoryId);
            if (ExistingCategory == null)
            {
                return NotFound("Category Not Available...Try Another ID");
            }
            ExistingCategory.CategoryName = newCategory.CategoryName;
            await _context.SaveChangesAsync();
            return Ok(ExistingCategory);
        }
        [HttpDelete("{CategoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int CategoryId)
        {
            var ExistingCategory = await _context.ProductCategories.SingleOrDefaultAsync(c => c.CategoryId == CategoryId);
            if (ExistingCategory == null)
            {
                return NotFound("Category Not Available...Try Another ID");
            }
            _context.ProductCategories.Remove(ExistingCategory);
            await _context.SaveChangesAsync();
            return Ok($"Category with ID: {CategoryId} deleted Sucessfully...");
        }
        [HttpGet("ProductWithAssCategory")]
        public async Task<IActionResult> GetProductsByCategory([FromQuery] int CategoryId)
        {
            var productAmongCategory = await _context.ProductCategories
                .Include(p => p.Products)
                .SingleOrDefaultAsync(c => c.CategoryId == CategoryId);
            if (productAmongCategory == null)
            {
                return NotFound($"Category with ID: {CategoryId} is not avaialble....Try Another Id...");
            }

           // var productModelAmongCategory = _mapper.Map<ProductCategorySM>(productAmongCategory);
            return Ok(productAmongCategory);
        }
    }
}
