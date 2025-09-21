namespace IssueTicketService.Core.Repositories
{
    public interface ITokenRepository
    {
        Task SaveRefreshToken(string refreshToken, Guid userId);
    }
}