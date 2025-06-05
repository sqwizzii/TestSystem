using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TestSystemAPI.Data
{
    public class TestSystemDbContextFactory : IDesignTimeDbContextFactory<TestSystemDbContext>
    {
        public TestSystemDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestSystemDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath("C:\\Users\\artem\\source\\repos\\TestSystem\\TestSystem")
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseNpgsql(connectionString);

            return new TestSystemDbContext(optionsBuilder.Options);
        }
    }
}
