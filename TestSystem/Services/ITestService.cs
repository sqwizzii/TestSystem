using TestSystem.Entities;

namespace TestSystem.Services
{
    public interface ITestService
    {
        Task<List<Test>> GetAllTestsAsync();
        Task<Test?> GetTestByIdAsync(int id);
        Task<Test> CreateTestAsync(Test test);
        Task UpdateTestAsync(Test test);
        Task DeleteTestAsync(int id);
    }
}
