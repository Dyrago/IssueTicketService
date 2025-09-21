using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.UserRoles
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IDbContextFactory<IssueTicketServiceDbContext> _dbContextFactory;

        public UserRoleRepository(IDbContextFactory<IssueTicketServiceDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task AddRoleToUser(Guid userId, int roleId)
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            var user = await dbContext.Users.Where(u => u.Id == userId)
                                            .Include(r => r.Roles)
                                            .FirstAsync();

            if (user == null)
                return;

            var role = await dbContext.Roles.Where(r => r.Id == roleId).FirstAsync();

            if (role == null)
                return;

            user.Roles.Add(role);
            await dbContext.SaveChangesAsync();
        }

        public Task RemoveRoleFromUSer(Guid userId, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}