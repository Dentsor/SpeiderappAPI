using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeiderappAPI.Models;

namespace SpeiderappAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        private readonly DBContext _context;

        public BadgeController(DBContext context)
        {
            _context = context;
        }

        // GET: api/v1/Badge
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Badge>>> GetBadges()
        {
            return await _context.BadgeList.ToListAsync();
        }

        // GET: api/v1/Badge/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Badge>> GetBadge(long id)
        {
            var badge = await _context.BadgeList.FindAsync(id);

            if (badge == null)
            {
                return NotFound();
            }

            return badge;
        }

        // PUT: api/v1/Badge/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBadge(long id, Badge badge)
        {
            if (id != badge.Id)
            {
                return BadRequest();
            }

            _context.Entry(badge).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BadgeExists(id))
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

        // POST: api/v1/Badge
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Badge>> PostBadge(Badge badge)
        {
            _context.BadgeList.Add(badge);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBadge", new { id = badge.Id }, badge);
        }

        // DELETE: api/v1/Badge/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Badge>> DeleteBadge(long id)
        {
            var badge = await _context.BadgeList.FindAsync(id);
            if (badge == null)
            {
                return NotFound();
            }

            _context.BadgeList.Remove(badge);
            await _context.SaveChangesAsync();

            return badge;
        }

        private bool BadgeExists(long id)
        {
            return _context.BadgeList.Any(e => e.Id == id);
        }
    }
}
