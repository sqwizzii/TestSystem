using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TestSystem.Models;

namespace TestSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAttempt> UserAttempts { get; set; }
    }
}
