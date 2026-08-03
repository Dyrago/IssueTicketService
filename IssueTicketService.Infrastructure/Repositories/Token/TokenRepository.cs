using IssueTicketService.Core.Entities;
using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.Token
{
    public class TokenRepository : ITokenRepository
    {

        private readonly IDbContextFactory<IssueTicketServiceDbContext> _contextFactory;

        public TokenRepository(IDbContextFactory<IssueTicketServiceDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task SaveRefreshToken(RefreshToken refreshToken)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            await context.RefreshTokens.AddAsync(refreshToken);

            await context.SaveChangesAsync();
        }

        public async Task Revoke(RefreshToken refreshToken, Guid userId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            await context.RefreshTokens.Where(rt => rt.TokenValue == refreshToken.TokenValue && rt.UserId == userId)
                                       .ExecuteUpdateAsync(t => t.SetProperty(t => t.IsRevoked, true)
                                                                  .SetProperty(t => t.RevokeDate, DateTime.Now));

            await context.SaveChangesAsync();
        }
    }
}