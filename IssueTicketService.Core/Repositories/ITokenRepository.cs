using IssueTicketService.Core.Entities;

namespace IssueTicketService.Core.Repositories
{
    public interface ITokenRepository
    {
        Task<RefreshToken?> GetRefreshToken(string refreshToken);
        Task SaveRefreshToken(RefreshToken refreshToken);
        Task Revoke(RefreshToken refreshToken, Guid userId);
    }
}