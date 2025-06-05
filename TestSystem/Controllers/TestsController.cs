using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSystemAPI.Data;
using TestSystemAPI.Models;

namespace TestSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TestsController : ControllerBase
    {
        private readonly TestSystemDbContext _context;

        public TestsController(TestSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTests()
        {
            var tests = await _context.Tests.Include(t => t.Questions).ToListAsync();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTest(int id)
        {
            var test = await _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (test == null)
                return NotFound();

            return Ok(test);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateTest(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTest), new { id = test.Id }, test);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateTest(int id, Test test)
        {
            if (id != test.Id)
                return BadRequest();

            _context.Entry(test).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteTest(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
                return NotFound();

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
