using TestSystem.Entities;

public interface ITeacherService
{
    Task CreateTestAsync(Test test);
    Task AddQuestionAsync(Question question);
}
