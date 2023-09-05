using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomCategoriesController : ControllerBase
    {
        private readonly LodgeSystemManagementContext _context;

        public RoomCategoriesController(LodgeSystemManagementContext context)
        {
            _context = context;
        }

        // GET: api/RoomCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomCategory>>> GetRoomCategories()
        {
          if (_context.RoomCategories == null)
          {
              return NotFound();
          }
            return await _context.RoomCategories.ToListAsync();
        }

        // GET: api/RoomCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomCategory>> GetRoomCategory(int id)
        {
          if (_context.RoomCategories == null)
          {
              return NotFound();
          }
            var roomCategory = await _context.RoomCategories.FindAsync(id);

            if (roomCategory == null)
            {
                return NotFound();
            }

            return roomCategory;
        }

        // PUT: api/RoomCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomCategory(int id, RoomCategory roomCategory)
        {
            if (id != roomCategory.CategoryId)
            {
                return BadRequest();
            }

            _context.Entry(roomCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomCategoryExists(id))
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

        // POST: api/RoomCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomCategory>> PostRoomCategory(RoomCategory roomCategory)
        {
          if (_context.RoomCategories == null)
          {
              return Problem("Entity set 'LodgeSystemContext.RoomCategories'  is null.");
          }
            _context.RoomCategories.Add(roomCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomCategory", new { id = roomCategory.CategoryId }, roomCategory);
        }

        // DELETE: api/RoomCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomCategory(int id)
        {
            if (_context.RoomCategories == null)
            {
                return NotFound();
            }
            var roomCategory = await _context.RoomCategories.FindAsync(id);
            if (roomCategory == null)
            {
                return NotFound();
            }

            _context.RoomCategories.Remove(roomCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomCategoryExists(int id)
        {
            return (_context.RoomCategories?.Any(e => e.CategoryId == id)).GetValueOrDefault();
        }
    }
}
