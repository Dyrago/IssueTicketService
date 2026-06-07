using IssueTicketService.Application.Services.Account;
using IssueTicketService.Application.Services.Administration;
using IssueTicketService.Application.Services.Authentication;
using IssueTicketService.Application.Services.Role;
using IssueTicketService.Application.Services.Ticket;
using IssueTicketService.Application.Services.Token;
using IssueTicketService.Application.Services.User;
using IssueTicketService.Application.Services.UserRole;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IssueTicketService.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.TryAddScoped<ITokenProvider, TokenProvider>();
            services.TryAddScoped<ITokenService, TokenService>();
            services.TryAddScoped<ITicketService, TicketService>();
            services.TryAddScoped<IAuthenticationService, AuthenticationService>();
            services.TryAddScoped<IUserService, UserService>();
            services.TryAddScoped<IRoleService, RoleService>();
            services.TryAddScoped<IUserRoleService, UserRoleService>();
            services.TryAddScoped<IAdministrationService, AdministrationService>();
            services.TryAddScoped<ITicketCommentService, TicketCommentService>();
            services.TryAddScoped<IAccountService, AccountService>();
        }
    }
}