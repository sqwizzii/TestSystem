using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    [ApiController]
    [Route("api/tests")]
    public class TestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTests()
        {
            var tests = _context.Tests.Select(t => new { t.Id, t.Title }).ToList();
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public IActionResult GetTest(int id)
        {
            var test = _context.Tests
                .Include(t => t.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefault(t => t.Id == id);

            if (test == null)
                return NotFound();

            // вручну мапимо до DTO, щоб уникнути циклів
            var result = new Test
            {
                Id = test.Id,
                Title = test.Title,
                Questions = test.Questions.Select(q => new Question
                {
                    Text = q.Text,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        Text = a.Text
                    }).ToList()
                }).ToList()
            };

            return Ok(result);
        }

    }

}
