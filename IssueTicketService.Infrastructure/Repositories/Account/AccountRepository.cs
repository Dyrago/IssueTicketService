using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.Account
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbContextFactory<IssueTicketServiceDbContext> _dbContextFactory;
        public AccountRepository(IDbContextFactory<IssueTicketServiceDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task ChangePassword(Guid userId, string newPasswordHash, string salt)
        {
            var connection = await _dbContextFactory.CreateDbContextAsync();

            await connection.Users.Where(u => u.Id == userId)
            .ExecuteUpdateAsync(u => u.SetProperty(u => u.Password, newPasswordHash)
                                      .SetProperty(u => u.Salt, salt));

            await connection.SaveChangesAsync();
        }

        public async Task Delete(Guid userId)
        {
            var connection = await _dbContextFactory.CreateDbContextAsync();

            await connection.Users.Where(u => u.Id == userId).ExecuteDeleteAsync();
        }
    }
}