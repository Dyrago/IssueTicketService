using IssueTicketService.Application.Dto.Ticket;

namespace IssueTicketService.Application.Services.Ticket
{
    public interface ITicketCommentService
    {
        Task<TicketCommentDto?> Get(int id);
        Task Create(TicketCommentCreateDto comment, Guid userId);
        Task<IEnumerable<Core.Entities.Comment>> GetAll(int ticketId, int page, int size);
        Task Delete(int id);
        Task Edit(int id, Core.Entities.Comment comment);
    }
}