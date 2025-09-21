namespace IssueTicketService.Application.Services.Role
{
    public interface IRoleService
    {
        Task<Core.Entities.Role> GetById(int id);
        Task<Core.Entities.Role> GetByName(string name);
    }
}