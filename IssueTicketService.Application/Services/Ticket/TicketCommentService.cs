using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Core.Entities;
using IssueTicketService.Core.Enums;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Ticket
{
    public class TicketCommentService : ITicketCommentService
    {
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly ITicketService _ticketService;
        private readonly IUserRepository _userRepository;
        public TicketCommentService(
                ITicketCommentRepository ticketCommentRepository,
                ITicketService ticketService,
                IUserRepository userRepository)
        {
            _ticketCommentRepository = ticketCommentRepository;
            _ticketService = ticketService;
            _userRepository = userRepository;
        }

        public async Task Create(TicketCommentCreateDto comment, Guid userId, RoleEnum userRole)
        {
            var ticket = await _ticketService.GetById(comment.TicketId) ?? throw new Exception("Resource not found.");

            if (ticket?.Status == TicketStatusEnum.Resolved.ToString() ||
                ticket?.Status == TicketStatusEnum.Closed.ToString())
                throw new Exception("Can not comment on tickets with this status.");
            
            bool canComment = false;

            if (userRole == RoleEnum.Admin)
                canComment = true;

            else if (userRole == RoleEnum.Moderator && ticket?.ResolverId == userId)
                canComment = true;

            else if (userRole == RoleEnum.User && ticket?.ReporterId == userId)
                canComment = true;
            
            if (canComment)
                await _ticketCommentRepository.Create(new Core.Entities.Comment()
                {
                    CreateDate = DateTime.Now,
                    TicketId = comment.TicketId,
                    UserId = userId,
                    Content = comment.Content
                });
            else
                throw new Exception("Comment can't be created.");
        }

        public async Task Delete(int id, Guid? userId, RoleEnum userRole)
        {
            var currentComment = await _ticketCommentRepository.Get(id) ?? throw new Exception("Resource not found");
            var ticket = await _ticketService.GetById(currentComment.TicketId) ?? throw new Exception("Resource not found.");

            var canDelete = false;

            if (userRole == RoleEnum.Admin)
                canDelete = true;

            if (userRole == RoleEnum.Moderator && ticket?.ResolverId == userId)
                canDelete = true;

            if (userRole == RoleEnum.User && 
                ticket?.ReporterId == userId &&
                currentComment.UserId == userId)
                canDelete = true;

            if (canDelete)
                await _ticketCommentRepository.Delete(id);
            else
                throw new Exception("No accsess to execute this action");
        }

        public async Task Edit(TicketCommentEditDto comment, Guid userId, RoleEnum userRole)
        {
            var currentComment = await _ticketCommentRepository.Get(comment.CommentId) ?? throw new Exception("Resource not found");
            var ticket = await _ticketService.GetById(currentComment.TicketId) ?? throw new Exception("Resource not found.");

            if (ticket.Status == TicketStatusEnum.Resolved.ToString() ||
                ticket.Status == TicketStatusEnum.Closed.ToString())
                    throw new Exception("Can not edit comments on resolved or closed tickets.");

            var canEdit = false;

            if (userRole == RoleEnum.Admin)
                canEdit = true;
            else if (userRole == RoleEnum.Moderator &&
                     ticket.ResolverId == userId)
                canEdit = true;
            else if (userRole == RoleEnum.User &&
                     ticket.ReporterId == userId &&
                     currentComment.UserId == userId)
                canEdit = true;

            if (canEdit)
                await _ticketCommentRepository.Edit(currentComment.Id, comment.Content);
            else
                throw new Exception("No access to execute this action");
        }

        public async Task<TicketCommentSearchResultDto> Search(TicketCommentsSearchFilter filter, Guid userId, RoleEnum userRole)
        {
            var ticket = await _ticketService.GetById(filter.ticketId) ?? throw new Exception("Resource not found.");
            
            var comments = Enumerable.Empty<Comment>();
            var totalRowsCount = 0;

            bool canGet = false;

            if (userRole == RoleEnum.Admin)
                canGet = true;
            
            if (userRole == RoleEnum.Moderator && (
                ticket.ResolverId == userId ||
                ticket.ResolverId == null))
                canGet = true;

            if (userRole == RoleEnum.User &&
                ticket.ReporterId == userId)
                canGet = true;

            if (canGet == true)
                (comments, totalRowsCount) = await _ticketCommentRepository.Search(filter.ticketId, filter.page, filter.size);

            return new TicketCommentSearchResultDto()
            {
                Comments = comments.Select(c => new TicketCommentDto(c)),
                TotalRowsCount = totalRowsCount
            };
        }
    }
}