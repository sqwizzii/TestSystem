using TestSystem.Data;
using TestSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace TestSystem.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;

        public TeacherService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task AddQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task AddAnswerAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Test> GetAllTests()
        {
            return _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .ToList();
        }
    }
}
