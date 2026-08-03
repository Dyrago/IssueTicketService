using IssueTicketService.Application.Repositories.Administration;
using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using IssueTicketService.Infrastructure.Repositories.Account;
using IssueTicketService.Infrastructure.Repositories.Administration;
using IssueTicketService.Infrastructure.Repositories.Role;
using IssueTicketService.Infrastructure.Repositories.Ticket;
using IssueTicketService.Infrastructure.Repositories.User;
using IssueTicketService.Infrastructure.Repositories.UserRoles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IssueTicketService.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContextFactory<IssueTicketServiceDbContext>(options =>
{
    options.UseNpgsql(connectionString, opt =>
    {
        opt.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(5),
            errorCodesToAdd: null
        );
        opt.MigrationsAssembly(typeof(IssueTicketServiceDbContext).Assembly.GetName().Name);
    });
});

            services.TryAddScoped<ITicketRepository, TicketRepository>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IRoleRepository, RoleRepository>();
            services.TryAddScoped<IUserRoleRepository, UserRoleRepository>();
            services.TryAddScoped<ITicketCommentRepository, TicketCommentRepository>();
            services.TryAddScoped<IAccountRepository, AccountRepository>();
            services.TryAddScoped<IAdministrationRepository, AdministrationRepository>();
        }
    }
}