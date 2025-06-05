using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestSystemAPI.Data;
using TestSystemAPI.DTOs;
using TestSystemAPI.Models;

namespace TestSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TestAttemptsController : ControllerBase
    {
        private readonly TestSystemDbContext _context;

        public TestAttemptsController(TestSystemDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitAttempt(TestAttemptCreateDTO dto)
        {
            var test = await _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == dto.TestId);

            if (test == null)
                return NotFound("Test not found");

            int score = 0;

            foreach (var question in test.Questions)
            {
                var correctAnswers = question.Answers.Where(a => a.IsCorrect).Select(a => a.Id).ToHashSet();

                var selectedForQuestion = dto.SelectedAnswerIds.Intersect(question.Answers.Select(a => a.Id));

                if (selectedForQuestion.All(id => correctAnswers.Contains(id)) &&
                    correctAnswers.All(id => selectedForQuestion.Contains(id)))
                {
                    score++;
                }
            }

            var attempt = new Attempt
            {
                UserId = dto.UserId,
                TestId = dto.TestId,
                StartTime = DateTime.UtcNow,   // Приклад, можна адаптувати
                EndTime = DateTime.UtcNow,
                Score = score
            };

            _context.Attempts.Add(attempt);
            await _context.SaveChangesAsync();

            return Ok(new { attempt.Id, attempt.Score });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserAttempts(int userId)
        {
            var attempts = await _context.Attempts
                .Where(a => a.UserId == userId)
                .Include(a => a.Test)
                .ToListAsync();

            return Ok(attempts);
        }
    }
}
