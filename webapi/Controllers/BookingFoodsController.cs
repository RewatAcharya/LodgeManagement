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
    public class BookingFoodsController : ControllerBase
    {
        private readonly LodgeSystemManagementContext _context;

        public BookingFoodsController(LodgeSystemManagementContext context)
        {
            _context = context;
        }

        // GET: api/BookingFoods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoodInvoice>>> GetBookingFoods()
        {
          if (_context.FoodInvoices == null)
          {
              return NotFound();
          }
            return await _context.FoodInvoices.ToListAsync();
        }

        // GET: api/BookingFoods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodDetail>> GetBookingFood(int id)
        {
          if (_context.FoodDetails == null)
          {
              return NotFound();
          }
            var bookingFood = await _context.FoodDetails.FindAsync(id);

            if (bookingFood == null)
            {
                return NotFound();
            }

            return bookingFood;
        }

        // PUT: api/BookingFoods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingFood(int id, FoodDetail bookingFood)
        {
            if (id != bookingFood.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookingFood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingFoodExists(id))
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

        // POST: api/BookingFoods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodDetail>> PostBookingFood(FoodDetail bookingFood)
        {
          if (_context.FoodDetails == null)
          {
              return Problem("Entity set 'LodgeSystemContext.BookingFoods'  is null.");
          }
            _context.FoodDetails.Add(bookingFood);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingFood", new { id = bookingFood.Id }, bookingFood);
        }

        // DELETE: api/BookingFoods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingFood(int id)
        {
            if (_context.FoodDetails == null)
            {
                return NotFound();
            }
            var bookingFood = await _context.FoodDetails.FindAsync(id);
            if (bookingFood == null)
            {
                return NotFound();
            }

            _context.FoodDetails.Remove(bookingFood);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingFoodExists(int id)
        {
            return (_context.FoodDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
