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
    public class StocksController : ControllerBase
    {
        private readonly OnlineOrderContext _context;
        private ILogger _logger;

        public StocksController(OnlineOrderContext context, ILogger<StocksController> logger )
        {
            _context = context ?? throw new ArgumentNullException("context");
            _logger = logger ?? throw new ArgumentNullException("logger");
        }



      
        [HttpGet("Product/{productId}")]
        public async Task<ActionResult<IList<Stock>>> GetProductStock(int productId)
        {
            try
            {
                if (_context.Stocks == null)
                {
                    return NotFound();
                }
                var stock = await _context.Stocks.Where(c => c.ProductId == productId).ToListAsync();

                if (stock == null)
                {
                    return NotFound("Product is invalid.");
                }

                return stock;
            }
            catch(Exception ex)
            {

                throw;
            }
        }


        //// GET: api/Stocks/5

        
        [HttpGet("Location/{locationId}")]
      
        public async Task<ActionResult<IList<Stock>>> GetLocationStock(int locationId)
        {
            if (_context.Stocks == null)
            {
                return NotFound();
            }
            var stock = await _context.Stocks.Where(c => c.LocationId == locationId).ToListAsync();

            if (stock == null)
            {
                return NotFound();
            }

            return stock;
        }



        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Add")]
        public async Task<ActionResult<Stock>> AddStock(StockDto stockDto)
        {
            if (stockDto.Quantity <=0) return BadRequest("Quanty should be greater than 0");

            if (stockDto.ProductId==0 && stockDto.LocationId==0) return BadRequest();

              var product= _context.Products.FirstOrDefault(c => c.Id == stockDto.ProductId );

             if (product == null) return NotFound("Product not found");


              var location = _context.Locations.FirstOrDefault(c => c.Id == stockDto.LocationId);

              if (location == null) return NotFound("Location not found");


            var stock = _context.Stocks.FirstOrDefault(c => c.ProductId == stockDto.ProductId && c.LocationId == stockDto.LocationId);

            if (stock == null)
            {
                stock = stockDto.Create();
                _context.Stocks.Add(stock);
            }
            else
            {
                _context.Entry(stock).State = EntityState.Modified;
                stock.Quantity += stockDto.Quantity;

            }

            
            await _context.SaveChangesAsync();

            return new OkObjectResult(stock);
        }

        // POST: api/Stocks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Remove")]
        public async Task<ActionResult<Stock>> RemoveStock(StockDto stockDto)
        {

            if (stockDto.Quantity <= 0) return BadRequest("Quanty should be greater than 0");

            if (stockDto.ProductId == 0 && stockDto.LocationId == 0) return BadRequest();

            var product = _context.Products.FirstOrDefault(c => c.Id == stockDto.ProductId);

            if (product == null) return NotFound("Product not found");


            var location = _context.Locations.FirstOrDefault(c => c.Id == stockDto.LocationId);

            if (location == null) return NotFound("Location not found");


            var stock = _context.Stocks.FirstOrDefault(c => c.ProductId == stockDto.ProductId && c.LocationId == stockDto.LocationId);

            if (stock == null)
            {
                return NotFound($"Product {product.Name} is not in stock at location {location.Name}");
            }
            else
            {
                if (stock.Quantity < stockDto.Quantity) return new  OkObjectResult("the stock is not enough");
                if (stock.Quantity == stockDto.Quantity)
                {
                    _context.Entry(stock).State = EntityState.Deleted;
                    stock.Quantity = 0;
                 


                }
                else
                {
                    _context.Entry(stock).State = EntityState.Modified;
                    stock.Quantity -= stockDto.Quantity;
                    

                }
            }

            await _context.SaveChangesAsync();

            return new OkObjectResult(stock);

        }



        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
