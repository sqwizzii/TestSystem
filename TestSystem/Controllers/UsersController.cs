using Microsoft.AspNetCore.Mvc;
using TestSystemAPI.Data;
using TestSystemAPI.Models;
using TestSystemAPI.DTOs;
using System.Security.Cryptography;
using System.Text;

namespace TestSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly TestSystemDbContext _context; 

        public UsersController(TestSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Username == dto.Username))
                return BadRequest("Користувач з таким логіном вже існує");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                FullName = dto.FullName,
                Email = dto.Email,
                Role = dto.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Реєстрація успішна");
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
