using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace IssueTicketService.Application.Dto.Ticket
{
    public class TicketRequestDto
    {
        [NotNull]
        [MinLength(1)]
        [MaxLength(120)]
        public string Title { get; set; } = string.Empty;
        [NotNull]
        [MinLength(1)]
        [MaxLength(500)]
        public string Content { get; set; } = string.Empty;
    }
}