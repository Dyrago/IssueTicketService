using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
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
            services.AddDbContextFactory<IssueTicketServiceDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("Default"));
            });

            services.TryAddScoped<ITicketRepository, TicketRepository>();
            services.TryAddScoped<IUserRepository, UserRepository>();
            services.TryAddScoped<IRoleRepository, RoleRepository>();
            services.TryAddScoped<IUserRoleRepository, UserRoleRepository>();
            services.TryAddScoped<ITicketCommentRepository, TicketCommentRepository>();
        }
    }
}