using Microsoft.EntityFrameworkCore;
using TestSystem.Models;

public class TestSystemContext : DbContext
{
    public TestSystemContext(DbContextOptions<TestSystemContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Attempt> Attempts { get; set; }
    public DbSet<Image> Images { get; set; }
}
