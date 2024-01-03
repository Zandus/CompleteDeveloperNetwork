using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompleteDeveloperNetwork.Data;
using CompleteDeveloperNetwork.Models;
using CompleteDeveloperNetwork.Data.Response;

namespace CompleteDeveloperNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDBContext _context;

        public UsersController(UserDBContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserRequestData user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound(); // Return a 404 response if the user with the specified ID is not found
            }

            //Check if the email, phone number, or username already exists
                if (await _context.Users.AnyAsync(u => u.Mail == user.Mail))
            {
                ModelState.AddModelError("Mail", "Email already exists.");
                return BadRequest(ModelState);
            }

            if (await _context.Users.AnyAsync(u => u.PhoneNumber == user.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                return BadRequest(ModelState);
            }

            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                ModelState.AddModelError("UserName", "Username already exists.");
                return BadRequest(ModelState);
            }

            // Update the attributes of the existing user
            UpdateUser(existingUser, user);

            try
            {
                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Handle concurrency conflict
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRequestData user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'UserDBContext.Users'  is null.");
            }

            // Check if the email, phone number, or username already exists
            if (await _context.Users.AnyAsync(u => u.Mail == user.Mail))
            {
                ModelState.AddModelError("Mail", "Email already exists.");
                return BadRequest(ModelState);
            }

            if (await _context.Users.AnyAsync(u => u.PhoneNumber == user.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
                return BadRequest(ModelState);
            }

            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                ModelState.AddModelError("UserName", "Username already exists.");
                return BadRequest(ModelState);
            }

            var userData = ConvertRequestDataToModel(user);

            _context.Users.Add(userData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = userData.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private User ConvertRequestDataToModel(UserRequestData data)
        {
            var user = new User()
            {
                Username = data.Username,
                Mail = data.Mail,
                PhoneNumber = data.PhoneNumber,
                Skillsets = data.Skillsets,
                Hobby = data.Hobby,
            };

            return user;
        }

        private User UpdateUser(User currentUser, UserRequestData data)
        {
            currentUser.Username = data.Username;
            currentUser.Mail = data.Mail;
            currentUser.PhoneNumber = data.PhoneNumber;
            currentUser.Skillsets = data.Skillsets;
            currentUser.Hobby = data.Hobby;

            return currentUser;
        }
    }
}
