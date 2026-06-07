using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Core.BusinessObjects;

namespace IssueTicketService.Application.Services.Ticket
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketResponseDto>> Search(TicketSearchFilter filter);
        Task<TicketResponseDto?> GetById(int id, Guid? userId = null);
        Task<IEnumerable<TicketResponseDto?>> GetByUserId(Guid userId);
        Task Create(TicketRequestDto ticket, Guid userId);
        Task Update(int id, TicketRequestDto ticket, Guid? userId = null);
        Task Delete(int id, Guid? userId = null);
        Task ChangeStatus(int taskId, int statusId);
    }
}