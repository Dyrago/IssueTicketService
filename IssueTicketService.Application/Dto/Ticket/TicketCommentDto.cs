using IssueTicketService.Application.Helpers;
using IssueTicketService.Core.Entities;

namespace IssueTicketService.Application.Dto.Ticket
{
    public class TicketCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserEmail { get; set; }

        public TicketCommentDto()
        {

        }

        public TicketCommentDto(Comment comment)
        {
            Id = comment.Id;
            Content = comment.Content;
            CreateDate = DateTimeFormater.ToPolishTime(comment.CreateDate);
            UserEmail = comment.User.Email;
        }
    }
}