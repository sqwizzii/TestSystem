using Microsoft.AspNetCore.Mvc;
using TestSystem.Data;
using TestSystem.Models;

namespace TestSystem.Controllers
{
    [ApiController]
    [Route("api/answers")]
    public class AnswersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AnswersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
        public IActionResult SubmitAnswers([FromBody] SubmissionDto dto)
        {
            var attempt = _context.UserAttempts
                .FirstOrDefault(a => a.UserId == dto.UserId && a.TestId == dto.TestId);

            if (attempt != null && attempt.AttemptCount >= 3)
                return BadRequest("Перевищено кількість спроб");

            if (attempt == null)
            {
                attempt = new UserAttempt
                {
                    UserId = dto.UserId,
                    TestId = dto.TestId,
                    AttemptCount = 1
                };
                _context.UserAttempts.Add(attempt);
            }
            else
            {
                attempt.AttemptCount++;
            }

            _context.SaveChanges();
            return Ok(new { success = true });
        }
    }
}
