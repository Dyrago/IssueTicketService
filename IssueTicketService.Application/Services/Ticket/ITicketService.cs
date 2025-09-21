using IssueTicketService.Application.Dto.Ticket;

namespace IssueTicketService.Application.Services.Ticket
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketResponseDto>> Get();
        Task<TicketResponseDto?> GetById(int id);
        Task<IEnumerable<TicketResponseDto?>> GetByUserId(Guid userId);
        Task Create(TicketRequestDto ticket, Guid userId);
        Task Update(int id, TicketRequestDto ticket);
        Task Delete(int id);
        Task ChangeStatus(int taskId, int statusId);
    }
}