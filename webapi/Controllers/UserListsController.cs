using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class UserListsController : ControllerBase
    {
        private readonly LodgeSystemManagementContext _context;

        public UserListsController(LodgeSystemManagementContext context)
        {
            _context = context;
        }

        // GET: api/UserLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserList>>> GetUserLists()
        {
          if (_context.UserLists == null)
          {
              return NotFound();
          }
            return await _context.UserLists.ToListAsync();
        }

        // GET: api/UserLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserList>> GetUserList(int id)
        {
          if (_context.UserLists == null)
          {
              return NotFound();
          }
            var userList = await _context.UserLists.FindAsync(id);

            if (userList == null)
            {
                return NotFound();
            }

            return userList;
        }

        // PUT: api/UserLists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserList(int id, UserList userList)
        {
            if (id != userList.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserListExists(id))
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


    // POST: api/UserLists
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
        public async Task<ActionResult<UserList>> PostUserList(UserList userList)
        {
          if (_context.UserLists == null)
          {
              return Problem("Entity set 'LodgeSystemContext.UserLists'  is null.");
          }
            _context.UserLists.Add(userList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserList", new { id = userList.UserId }, userList);
        }

        // DELETE: api/UserLists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserList(int id)
        {
            if (_context.UserLists == null)
            {
                return NotFound();
            }
            var userList = await _context.UserLists.FindAsync(id);
            if (userList == null)
            {
                return NotFound();
            }

            _context.UserLists.Remove(userList);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserListExists(int id)
        {
            return (_context.UserLists?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
