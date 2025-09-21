using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Ticket
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Create(TicketRequestDto dto, Guid userGuid)
        {
            var ticket = new Core.Entities.Ticket()
            {
                Title = dto.Title,
                Content = dto.Content,
                ReporterId = userGuid
            };

            await _ticketRepository.Create(ticket);
        }

        public async Task Delete(int id)
        {
            await _ticketRepository.Delete(id);
        }

        public async Task<IEnumerable<TicketResponseDto>> Get()
        {
            var tickets = await _ticketRepository.Get();

            return tickets.Select(t => new TicketResponseDto(t));
        }

        public async Task<TicketResponseDto> GetById(int id)
        {
            var ticket = await _ticketRepository.GetById(id);

            return new TicketResponseDto(ticket);
        }

        public async Task<IEnumerable<TicketResponseDto?>> GetByUserId(Guid userId)
        {
            var tickets = await _ticketRepository.GetByUserId(userId);

            return tickets.Select(t => new TicketResponseDto(t));
        }

        public async Task Update(int id, TicketRequestDto dto)
        {
            var ticket = new Core.Entities.Ticket()
            {
                Title = dto.Title,
                Content = dto.Content
            };

            await _ticketRepository.Update(id, ticket);
        }

        public async Task ChangeStatus(int taskId, int statusId)
        {
            await _ticketRepository.ChangeStatus(taskId, statusId);
        }
    }
}