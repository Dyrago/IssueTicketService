namespace IssueTicketService.Core.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Core.Entities.Ticket>> Get();
        Task<Core.Entities.Ticket?> GetById(int id);
        Task<IEnumerable<Core.Entities.Ticket?>> GetByUserId(Guid userId);
        Task Create(Core.Entities.Ticket ticket);
        Task Update(int id, Core.Entities.Ticket ticket);
        Task Delete(int id);
        Task ChangeStatus(int taskId, int statusId);
    }
}