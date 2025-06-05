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
    public class AnswersController : ControllerBase
    {
        private readonly TestSystemDbContext _context;

        public AnswersController(TestSystemDbContext context)
        {
            _context = context;
        }

        [HttpGet("byquestion/{questionId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByQuestion(int questionId)
        {
            var answers = await _context.Answers.Where(a => a.QuestionId == questionId).ToListAsync();
            return Ok(answers);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAnswer(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByQuestion), new { questionId = answer.QuestionId }, answer);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateAnswer(int id, Answer answer)
        {
            if (id != answer.Id)
                return BadRequest();

            _context.Entry(answer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var answer = await _context.Answers.FindAsync(id);
            if (answer == null)
                return NotFound();

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
