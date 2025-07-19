using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace SchoolCatalog.Server.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;

        public UserController(SchoolCatalogContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Elev)
                .Include(u => u.Profesor)
                .ToListAsync();

            if (!users.Any())
                return NotFound("Nu s-au găsit utilizatori.");

            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users
                .Include(u => u.Elev)
                .Include(u => u.Profesor)
                .FirstOrDefaultAsync(u => u.IdUser == id);

            if (user == null)
                return NotFound($"Utilizatorul cu ID-ul {id} nu a fost găsit.");

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Datele utilizatorului sunt invalide.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.IdUser }, user);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.IdUser != id)
                return BadRequest("Datele sunt invalide sau ID-ul nu se potrivește.");

            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
                return NotFound($"Utilizatorul cu ID-ul {id} nu a fost găsit.");

            existingUser.Email = user.Email;
            existingUser.Parola = user.Parola;
            existingUser.Rol = user.Rol;
            existingUser.IdElev = user.IdElev;
            existingUser.IdProfesor = user.IdProfesor;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound($"Utilizatorul cu ID-ul {id} nu a fost găsit.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchUser(int id, [FromBody] JsonPatchDocument<User> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest("Documentul de patch nu poate fi null.");

            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound($"Utilizatorul cu ID-ul {id} nu a fost găsit.");

            patchDoc.ApplyTo(user, error =>
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] User credentials)
        {
            if (string.IsNullOrEmpty(credentials.Email) || string.IsNullOrEmpty(credentials.Parola))
                return BadRequest("Emailul și parola sunt obligatorii.");

            var user = await _context.Users
                .Include(u => u.Elev)
                .Include(u => u.Profesor)
                .FirstOrDefaultAsync(u => u.Email == credentials.Email && u.Parola == credentials.Parola);

            if (user == null)
                return Unauthorized("Email sau parolă incorecte.");

            return Ok(user);
        }
    }
}
