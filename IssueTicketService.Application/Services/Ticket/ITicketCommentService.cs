using IssueTicketService.Application.Dto.Ticket;

namespace IssueTicketService.Application.Services.Ticket
{
    public interface ITicketCommentService
    {
        Task Create(TicketCommentCreateDto comment, Guid userId);
    }
}