namespace IssueTicketService.Application.Services.UserRole
{
    public interface IUserRoleService
    {
         Task AddRoleToUser(Guid userId, int roleId);
        Task RemoveRoleFromUser(Guid Id, int roleId);
    }
}