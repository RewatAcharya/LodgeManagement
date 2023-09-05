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
    public class BookingDetailsController : ControllerBase
    {
        private readonly LodgeSystemManagementContext _context;

        public BookingDetailsController(LodgeSystemManagementContext context)
        {
            _context = context;
        }

        // GET: api/BookingDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomInvoice>>> GetBookingDetails()
        {
          if (_context.RoomInvoices == null)
          {
              return NotFound();
          }
            return await _context.RoomInvoices.ToListAsync();
        }

        // GET: api/BookingDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookingDetail>> GetBookingDetail(int id)
        {
          if (_context.BookingDetails == null)
          {
              return NotFound();
          }
            var bookingDetail = await _context.BookingDetails.FindAsync(id);

            if (bookingDetail == null)
            {
                return NotFound();
            }

            return bookingDetail;
        }

        // PUT: api/BookingDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookingDetail(int id, BookingDetail bookingDetail)
        {
            if (id != bookingDetail.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookingDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingDetailExists(id))
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

        // POST: api/BookingDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookingDetail>> PostBookingDetail(BookingDetail bookingDetail)
        {
          if (_context.BookingDetails == null)
          {
              return Problem("Entity set 'LodgeSystemContext.BookingDetails'  is null.");
          }
            _context.BookingDetails.Add(bookingDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookingDetail", new { id = bookingDetail.Id }, bookingDetail);
        }

        // DELETE: api/BookingDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookingDetail(int id)
        {
            if (_context.BookingDetails == null)
            {
                return NotFound();
            }
            var bookingDetail = await _context.BookingDetails.FindAsync(id);
            if (bookingDetail == null)
            {
                return NotFound();
            }

            _context.BookingDetails.Remove(bookingDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingDetailExists(int id)
        {
            return (_context.BookingDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
