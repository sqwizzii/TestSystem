using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSystemAPI.Data;
using TestSystemAPI.Models;
using TestSystemAPI.DTOs;
using TestSystemAPI.Models;

namespace TestSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttemptsController : ControllerBase
    {
        private readonly TestSystemDbContext _context;

        public AttemptsController(TestSystemDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Attempt>>> GetUserAttempts(int userId)
        {
            var attempts = await _context.Attempts
                .Where(a => a.UserId == userId)
                .Include(a => a.Test)
                .ToListAsync();

            return attempts;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Attempt>> SubmitAttempt(Attempt attempt)
        {
            attempt.Date = DateTime.UtcNow;
            _context.Attempts.Add(attempt);
            await _context.SaveChangesAsync();
            return Ok(attempt);
        }
    }
}
