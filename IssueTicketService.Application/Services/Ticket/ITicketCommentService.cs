using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Core.Enums;

namespace IssueTicketService.Application.Services.Ticket
{
    public interface ITicketCommentService
    {
        Task Create(TicketCommentCreateDto comment, Guid userId, RoleEnum userRole);
        Task<TicketCommentSearchResultDto> Search(TicketCommentsSearchFilter filter, Guid userId, RoleEnum userRole);
        Task Delete(int id, Guid? userId, RoleEnum userRole);
        Task Edit(TicketCommentEditDto comment, Guid userId, RoleEnum userRole);
    }
}