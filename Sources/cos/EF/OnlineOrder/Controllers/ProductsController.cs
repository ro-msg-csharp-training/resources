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
    public class ProductsController : ControllerBase
    {
        private readonly OnlineOrderContext _context;
        private readonly ILogger _logger;

        public ProductsController(OnlineOrderContext context, ILogger<ProductsController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProduct()
        {
            try
            {
                if (_context.Products == null)
                {
                    return NotFound();
                }
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProduct)} - unhandle err ");
                return Problem($"{nameof(GetProduct)} internall error");
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                if (_context.Products == null)
                {
                    return NotFound();
                }
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(GetProduct)} - unhandle err - ID:{id} ");
                return Problem($"{nameof(GetProduct)} internall error");
            }
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDto productDto)
        {
            try
            {
                if (id != productDto.Id)
                {
                    return BadRequest();
                }

                var productCategory = await _context.ProductCategories.FindAsync(productDto.CategoryId);
                if (productCategory == null)
                {
                    return NotFound($"productCategory with id {productDto.CategoryId} not found");
                }
                var product = productDto.Create(productCategory);

                _context.Entry(product).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
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
                _logger.LogError(ex, $"{nameof(PutProduct)} - unhandle err - ID:{id}| DTO:{productDto} ");
                return Problem($"{nameof(PutProduct)} internall error");
            }
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDto>> PostProduct(ProductDto productDto)
        {
            try
            {
                if (_context.Products == null)
                {
                    return Problem("Entity set 'OnlineOrderContext.Product'  is null.");
                }
                if (_context.ProductCategories == null)
                {
                    return Problem("Entity set 'OnlineOrderContext.Product'  is null.");
                }
                var productCategory = await _context.ProductCategories.FindAsync(productDto.CategoryId);
                if (productCategory == null)
                {
                    return NotFound($"productCategory with id {productDto.CategoryId} not found");
                }
                var product = productDto.Create(productCategory);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(PostProduct)} - unhandle err -  DTO:{productDto} ");
                return Problem($"{nameof(PostProduct)} internall error");
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (_context.Products == null)
                {
                    return NotFound();
                }
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(DeleteProduct)} - unhandle err -  ID:{id} ");
                return Problem($"{nameof(DeleteProduct)} internall error");
            }
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
