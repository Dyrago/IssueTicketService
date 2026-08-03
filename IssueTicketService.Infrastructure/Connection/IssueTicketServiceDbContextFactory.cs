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
           if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("POSTGRES_HOST")) &&
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")))
            {
                Env.TraversePath().Load();
            }

            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                var host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? "localhost";
                var port = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? "5432";
                var dbName = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? "TicketsDb";
                var user = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? "postgres";
                var pass = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? "postgres";

                connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={pass};";
            }

            var optionsBuilder = new DbContextOptionsBuilder<IssueTicketServiceDbContext>();
            optionsBuilder.UseNpgsql(connectionString, opt =>
            {
                opt.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorCodesToAdd: null
                );
                opt.MigrationsAssembly(typeof(IssueTicketServiceDbContext).Assembly.GetName().Name);
            });

            return new IssueTicketServiceDbContext(optionsBuilder.Options);
        }
    }
}

//Configuration above allows to use EF Core from class library
// we separate EF Core configuration from API or so called startup project