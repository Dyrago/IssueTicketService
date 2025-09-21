using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace IssueTicketService.Application.Dto.Ticket
{
    public class TicketCommentCreateDto
    {
        [NotNull]
        [MinLength(2)]
        [MaxLength(500)]
        public string Content { get; set; }

        public int TicketId { get; set; }
    }
}