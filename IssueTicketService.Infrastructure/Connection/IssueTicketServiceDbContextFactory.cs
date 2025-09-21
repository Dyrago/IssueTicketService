using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IssueTicketService.Infrastructure.Connection
{
    public class IssueTicketServiceDbContextFactory : IDesignTimeDbContextFactory<IssueTicketServiceDbContext>
    {
        public IssueTicketServiceDbContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../IssueTicketService.Api");

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<IssueTicketServiceDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new IssueTicketServiceDbContext(optionsBuilder.Options);
        }
    }
}