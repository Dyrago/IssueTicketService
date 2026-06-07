using IssueTicketService.Core.BusinessObjects;

namespace IssueTicketService.Core.Repositories
{
    public interface ITicketRepository
    {
        Task<IEnumerable<Core.Entities.Ticket>> Search(TicketSearchFilter filter);
        Task<Core.Entities.Ticket?> GetById(int id, Guid? userId = null);
        Task<IEnumerable<Core.Entities.Ticket?>> GetByUserId(Guid userId);
        Task Create(Core.Entities.Ticket ticket);
        Task Update(int id, Core.Entities.Ticket ticket, Guid? userId = null);
        Task Delete(int id, Guid? userId = null);
        Task ChangeStatus(int taskId, int statusId);
    }
}