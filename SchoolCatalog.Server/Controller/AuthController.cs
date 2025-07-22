using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolCatalog.Server.Data;
using SchoolCatalog.Server.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ModelUser = SchoolCatalog.Server.Model.User;

namespace SchoolCatalog.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SchoolCatalogContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(SchoolCatalogContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.Email) || string.IsNullOrWhiteSpace(newUser.Parola) || string.IsNullOrWhiteSpace(newUser.Rol))
                return BadRequest("Email, parola și rolul sunt obligatorii.");

            // Verificăm dacă emailul există deja
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
            if (existingUser != null)
                return BadRequest("Emailul există deja.");

            var user = new ModelUser
            {
                Email = newUser.Email,
                Parola = newUser.Parola,
                Rol = newUser.Rol,
                IdElev = null,
                IdProfesor = null
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                IdUser = user.IdUser,
                Email = user.Email,
                Rol = user.Rol,
                IdElev = user.IdElev,
                IdProfesor = user.IdProfesor
            };

            return CreatedAtAction(nameof(Login), new { email = user.Email }, userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<object>> Login([FromBody] LoginDto credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.Email) || string.IsNullOrWhiteSpace(credentials.Parola))
                return BadRequest("Email și parola sunt obligatorii.");

            var user = await _context.Users
                .Include(u => u.Elev)
                .Include(u => u.Profesor)
                .FirstOrDefaultAsync(u => u.Email == credentials.Email && u.Parola == credentials.Parola);

            if (user == null)
                return Unauthorized("Email sau parolă greșite.");

            var token = GenerateJwtToken(user);

            var userDto = new UserDto
            {
                IdUser = user.IdUser,
                Email = user.Email,
                Rol = user.Rol,
                IdElev = user.IdElev,
                IdProfesor = user.IdProfesor
            };

            return Ok(new { token, user = userDto });
        }

        private string GenerateJwtToken(ModelUser user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.IdUser.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
