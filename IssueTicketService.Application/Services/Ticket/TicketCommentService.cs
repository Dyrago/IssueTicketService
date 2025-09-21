
using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Ticket
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly ITicketCommentRepository _ticketCommentRepository;
        public TicketCommentService()
        {

        }

        public async Task Create(TicketCommentCreateDto comment, Guid userId)
        {
            await _ticketCommentRepository.Create(new Core.Entities.Comment()
            {
                CreateDate = DateTime.Now,
                TicketId = comment.TicketId,
                UserId = userId,
                Content = comment.Content
            });
        }
    }
}