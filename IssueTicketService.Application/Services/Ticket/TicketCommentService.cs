using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Core.Entities;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Ticket
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly IUserRepository _userRepository;
        public TicketCommentService(
                ITicketCommentRepository ticketCommentRepository,
                IUserRepository userRepository)
        {
            _ticketCommentRepository = ticketCommentRepository;
            _userRepository = userRepository;
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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(int id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<TicketCommentDto?> Get(int id)
        {
            var comment = await _ticketCommentRepository.Get(id);

            if (comment == null)
                return null;

            var user = await _userRepository.GetById(comment.UserId);

            if (user == null)
                return null;

            return new TicketCommentDto()
            {
                Id = comment.Id,
                Content = comment.Content,
                CreateDate = comment.CreateDate,
                UserEmail = user.Email
            };
        }

        public async Task<IEnumerable<Comment>> GetAll(int ticketId, int page, int size)
        {
            return await _ticketCommentRepository.GetAll(ticketId, page, size);
        }
    }
}