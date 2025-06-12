using TestSystem.Entities;

namespace TestSystem.Services
{
    public interface ITeacherService
    {
        Task CreateTestAsync(Test test);
        Task AddQuestionAsync(Question question);
        Task AddAnswerAsync(Answer answer);
        IEnumerable<Test> GetAllTests();

    }
}
