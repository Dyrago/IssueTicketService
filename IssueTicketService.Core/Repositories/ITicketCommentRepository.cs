using IssueTicketService.Core.Entities;

namespace IssueTicketService.Core.Repositories
{
    public interface ITicketCommentRepository
    {
        Task<Comment?> Get(int id);
        Task<(IEnumerable<Comment>, int)> Search(int ticketId, int page, int size);
        Task Create(Core.Entities.Comment comment);
        Task Delete(int id);
        Task Edit(int id, Core.Entities.Comment comment);
    }
}