using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.Role
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IDbContextFactory<IssueTicketServiceDbContext> _dbContextFactory;
        public RoleRepository(IDbContextFactory<IssueTicketServiceDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        
        public async Task<Core.Entities.Role> GetById(int id)
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            return await dbContext.Roles.Where(r => r.Id == id).FirstAsync();
        }

        public async Task<Core.Entities.Role> GetByName(string name)
        {
            using var dbContext = await _dbContextFactory.CreateDbContextAsync();

            return await dbContext.Roles.Where(r => r.Name == name).FirstAsync();
        }
    }
}