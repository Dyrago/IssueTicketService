namespace IssueTicketService.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.User?> GetById(Guid id);
        Task<Entities.User?> GetByEmail(string email);
        Task<Guid> Create(Entities.User user);
        Task Update(int id, Entities.User ticket);
        Task Delete(int id);
    }
}