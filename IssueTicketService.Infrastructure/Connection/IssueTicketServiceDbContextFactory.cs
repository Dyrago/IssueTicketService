using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace IssueTicketService.Infrastructure.Connection
{
    public class IssueTicketServiceDbContextFactory : IDesignTimeDbContextFactory<IssueTicketServiceDbContext>
    {
        public IssueTicketServiceDbContext CreateDbContext(string[] args)
        {
            Env.TraversePath().Load();   
            
            var host = Environment.GetEnvironmentVariable("POSTGRES_HOST");
            var port = Environment.GetEnvironmentVariable("POSTGRES_PORT");
            var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB");
            var user = Environment.GetEnvironmentVariable("POSTGRES_USER");
            var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");

            var connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={pass};";

            var optionsBuilder = new DbContextOptionsBuilder<IssueTicketServiceDbContext>();
            optionsBuilder.UseNpgsql(connectionString, opt =>
            {
                opt.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null
                );
            });

            return new IssueTicketServiceDbContext(optionsBuilder.Options);
        }
    }
}

//Configuration above allows to use EF Core from class library
// we separate EF Core configuration from API or so called startup project