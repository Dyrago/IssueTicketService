namespace IssueTicketService.Core.Repositories
{
    public interface IAccountRepository
    {
        public Task ChangePassword(Guid userId, string newPasswordHash, string salt);
        public Task Delete(Guid userId);
    }
}