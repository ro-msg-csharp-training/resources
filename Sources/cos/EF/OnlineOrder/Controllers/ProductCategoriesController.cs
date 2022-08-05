using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineOrder.Data;
using OnlineOrder.Model;

namespace OnlineOrder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly OnlineOrderContext _context;
        private ILogger _logger;

        public ProductCategoriesController(OnlineOrderContext context, ILogger<ProductCategoriesController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategory()
        {
            try
            {
                if (_context.ProductCategories == null)
                {
                    return NotFound();
                }
                return await _context.ProductCategories.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductCategory)} - unhandle err ");
                return Problem($"{nameof(GetProductCategory)} internall error");
            }
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto>> GetProductCategory(int id)
        {
            try
            {
                if (_context.ProductCategories == null)
                {
                    return NotFound();
                }
                var productCategory = await _context.ProductCategories.FindAsync(id);

                if (productCategory == null)
                {
                    return NotFound();
                }

                return productCategory;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProductCategory)} - unhandle  - ID:{id} ");
                return Problem($"{nameof(GetProductCategory)} internall error");
            }
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategoryDto productCategoryDto)
        {
            try
            {
                if (id != productCategoryDto.Id)
                {
                    return BadRequest();
                }

                var productCategory = productCategoryDto.Create();

                _context.Entry(productCategory).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(PutProductCategory)} - unhandle  - ID:{id} | DTO: {productCategoryDto}");
                return Problem($"{nameof(PutProductCategory)} internall error");
            }
        }

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDto>> PostProductCategory(ProductCategoryDto productCategoryDto)
        {
            try
            {
                if (_context.ProductCategories == null)
                {
                    return Problem("Entity set 'OnlineOrderContext.ProductCategory'  is null.");
                }
                var productCategory = productCategoryDto.Create();

                _context.ProductCategories.Add(productCategory);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProductCategory", new { id = productCategory.Id }, productCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(PostProductCategory)} - unhandle  -  DTO: {productCategoryDto}");
                return Problem($"{nameof(PostProductCategory)} internall error");
            }
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            try
            {
                if (_context.ProductCategories == null)
                {
                    return NotFound();
                }
                var productCategory = await _context.ProductCategories.FindAsync(id);
                if (productCategory == null)
                {
                    return NotFound();
                }

                _context.ProductCategories.Remove(productCategory);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(DeleteProductCategory)} - unhandle  - ID:{id}");
                return Problem($"{nameof(DeleteProductCategory)} internall error");
            }
        }

        private bool ProductCategoryExists(int id)
        {
            return (_context.ProductCategories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
