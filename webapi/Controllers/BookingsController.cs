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
    public class BookingsController : ControllerBase
    {
        private readonly LodgeSystemManagementContext _context;

        public BookingsController(LodgeSystemManagementContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
          if (_context.Bookings == null)
          {
              return NotFound();
          }
            return await _context.Bookings.ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
          if (_context.Bookings == null)
          {
              return NotFound();
          }
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }
             
            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.BookingId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
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

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult PostBooking([FromBody] DTO bookingDto)
        {
          if (bookingDto == null)
          {
              return Problem("Entity set 'LodgeSystemContext.Bookings'  is null.");
          }
          
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Booking booking = new Booking
                    {
                        BookingDate = bookingDto.BookingDate,
                        CheckInDate = bookingDto.CheckInDate,
                        CheckOutDate = bookingDto.CheckOutDate,
                        NoOfPerson = bookingDto.NoOfPerson,
                        TotalRooms = bookingDto.TotalRooms,
                        DepositAmount = bookingDto.DepositAmount,
                        UserId = bookingDto.UserId
                    };

                    _context.Bookings.Add(booking);
                    _context.SaveChanges();

                    foreach (var item in bookingDto.RoomId)
                    {
                        BookingDetail d = new BookingDetail
                        {
                            BookingId = booking.BookingId,
                            RoomId = item
                        };
                        _context.BookingDetails.Add(d);
                    }
                   
                    _context.SaveChanges();
                    transaction.Commit();

                    return Ok(new { Message = "Booking created successfully." });
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(new { Error = ex.Message });
                }
            }
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            if (_context.Bookings == null)
            {
                return NotFound();
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return (_context.Bookings?.Any(e => e.BookingId == id)).GetValueOrDefault();
        }
    }
}
