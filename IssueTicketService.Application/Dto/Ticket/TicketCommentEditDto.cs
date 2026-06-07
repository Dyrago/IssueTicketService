using System.ComponentModel.DataAnnotations;

namespace IssueTicketService.Application.Dto.Ticket
{
    public class TicketCommentEditDto
    {
        public int TicketId { get; set; }
        public int CommentId { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(500)]
        public string Content{ get; set; }
    }
}