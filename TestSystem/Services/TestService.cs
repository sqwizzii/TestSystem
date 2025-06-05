using Microsoft.EntityFrameworkCore;
using TestSystem.Data;
using TestSystem.Entities;

namespace TestSystem.Services
{
    public class TestService : ITestService
    {
        private readonly ApplicationDbContext _context;

        public TestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Test>> GetAllTestsAsync()
        {
            return await _context.Tests.Include(t => t.Questions)
                                       .ThenInclude(q => q.Answers)
                                       .ToListAsync();
        }

        public async Task<Test?> GetTestByIdAsync(int id)
        {
            return await _context.Tests.Include(t => t.Questions)
                                       .ThenInclude(q => q.Answers)
                                       .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Test> CreateTestAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
            return test;
        }

        public async Task UpdateTestAsync(Test test)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTestAsync(int id)
        {
            var test = await _context.Tests.FindAsync(id);
            if (test != null)
            {
                _context.Tests.Remove(test);
                await _context.SaveChangesAsync();
            }
        }
    }
}
