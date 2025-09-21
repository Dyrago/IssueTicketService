namespace IssueTicketService.Core.Repositories
{
    public interface IRoleRepository
    {
        Task<Core.Entities.Role> GetById(int id);
        Task<Core.Entities.Role> GetByName(string name);
    }
}