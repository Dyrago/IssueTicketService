using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Core.BusinessObjects;

namespace IssueTicketService.Application.Services.Ticket
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketResponseDto>> Search(TicketSearchFilter filter);
        Task<TicketResponseDto?> GetById(int id);
        Task<IEnumerable<TicketResponseDto?>> GetByUserId(Guid userId);
        Task Create(TicketRequestDto ticket, Guid userId);
        Task Update(int id, TicketRequestDto ticket);
        Task Delete(int id);
        Task ChangeStatus(int taskId, int statusId);
    }
}