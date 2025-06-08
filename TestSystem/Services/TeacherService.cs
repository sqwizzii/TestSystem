using TestSystem.Data;
using TestSystem.Entities;

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
}
