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
    public class LocationsController : ControllerBase
    {
        private readonly OnlineOrderContext _context;
        private readonly ILogger _logger;

        public LocationsController(OnlineOrderContext context, ILogger<LocationsController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.LogInformation("LocationsController created");
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDto>>> GetLocations()
        {
            try
            {
                if (_context.Locations == null)
                {
                    return NotFound();
                }

                return await _context.Locations.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetLocation unhandle err for {nameof(GetLocations)}");
                return Problem($"{nameof(GetLocations)} internall error");
            }
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetLocation(int id)
        {
            try
            {
                if (_context.Locations == null)
                {
                    return NotFound();
                }
                var location = await _context.Locations.FindAsync(id);

                if (location == null)
                {
                    return NotFound();
                }

                return location;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetLocation unhandle err for {nameof(GetLocation)} - ID:{id}");
                return Problem($"{nameof(GetLocation)} internall error");
            }
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, LocationDto locationDto)
        {
            try
            {
                if (id != locationDto.Id)
                {
                    return BadRequest();
                }
                var location = locationDto.Create();

                _context.Entry(location).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(id))
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
                _logger.LogError(ex, $"GetLocation unhandle err for {nameof(PutLocation)} + DTO: {locationDto} ");
                return Problem($"{nameof(PutLocation)} internall error");
            }
        }

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LocationDto>> PostLocation(LocationDto locationDto)
        {
            try
            {
                if (_context.Locations == null)
                {
                    return Problem("Entity set 'OnlineOrderContext.Locations'  is null.");
                }
                Location location = locationDto.Create();
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetLocation", new { id = location.Id }, location);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetLocation unhandle err for{nameof(PostLocation)} + DTO: {locationDto} ");
                return Problem($"{nameof(PostLocation)} internall error");
            }
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                if (_context.Locations == null)
                {
                    return NotFound();
                }
                var location = await _context.Locations.FindAsync(id);
                if (location == null)
                {
                    return NotFound();
                }

                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetLocation unhandle err for {nameof(DeleteLocation)} - ID:{id}");
                return Problem($"{nameof(DeleteLocation)} internall error");
            }
        }

        private bool LocationExists(int id)
        {
            return (_context.Locations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
