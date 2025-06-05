using Microsoft.EntityFrameworkCore;
using TestSystemAPI.Models;

namespace TestSystemAPI.Data
{
    public class TestSystemDbContext : DbContext
    {
        public TestSystemDbContext(DbContextOptions<TestSystemDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
