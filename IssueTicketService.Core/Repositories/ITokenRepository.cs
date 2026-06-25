using IssueTicketService.Core.Entities;

namespace IssueTicketService.Core.Repositories
{
    public interface ITokenRepository
    {
        Task SaveRefreshToken(RefreshToken refreshToken);
        Task Revoke(RefreshToken refreshToken, Guid userId);
    }
}