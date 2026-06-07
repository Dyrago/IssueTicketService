namespace IssueTicketService.Core.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.User?> GetById(Guid id);
        Task<Entities.User?> GetByEmail(string email);
        Task<Guid> Create(Entities.User user);
        Task Update(Guid id, Entities.User user);
        Task Delete(Guid id);
    }
}