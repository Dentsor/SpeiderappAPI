using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeiderappAPI.Database;
using SpeiderappAPI.Models;

namespace SpeiderappAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ApiContext _context;

        public ResourceController(ApiContext context)
        {
            _context = context;
        }

        // GET: api/Resource
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resource>>> GetResource()
        {
            return await _context.Resources.ToListAsync();
        }

        // GET: api/Resource/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resource>> GetResource(long id)
        {
            var resource = await _context.Resources.FindAsync(id);

            if (resource == null)
            {
                return NotFound();
            }

            return resource;
        }

        // GET: api/Resource/badge/5
        [HttpGet("badge/{id}")]
        public async Task<ActionResult<Requirement>> GetBadgeFor(long id)
        {
            var resource = await _context.Resources.FindAsync(-1L);

            if (resource == null)
            {
                return NotFound();
            }

            return resource.Requirement;
        }

        // PUT: api/Resource/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResource(long id, Resource resource)
        {
            if (id != resource.ResourceID)
            {
                return BadRequest();
            }

            _context.Entry(resource).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResourceExists(id))
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

        // POST: api/Resource
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Resource>> PostResource(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResource", new { id = resource.ResourceID }, resource);
        }

        // DELETE: api/Resource/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(long id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ResourceExists(long id)
        {
            return _context.Resources.Any(e => e.ResourceID == id);
        }
    }
}
