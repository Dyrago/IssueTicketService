namespace IssueTicketService.Core.Repositories
{
    public interface IUserRoleRepository
    {
        Task AddRoleToUser(Guid userId, int roleId);
        Task RemoveRoleFromUSer(Guid userId, int roleId);
    }
}