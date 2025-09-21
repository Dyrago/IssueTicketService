using IssueTicketService.Core.Entities;
using IssueTicketService.Core.Enums;

namespace IssueTicketService.Application.Dto.Ticket
{
    public class TicketResponseDto
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<Comment> Comments { get; set; }
        public Guid ReporterId { get; set; }
        public Guid? ResolverId { get; set; }
        public string Status { get; set; }

        public TicketResponseDto()
        {
            Comments = new List<Comment>();
        }

        public TicketResponseDto(Core.Entities.Ticket ticket)
        {
            Id = ticket.Id;
            CreateDate = ticket.CreateDate;
            Title = ticket.Title;
            Content = ticket.Content;
            ReporterId = ticket.ReporterId;
            ResolverId = ticket.ResolverId;
            Status = ticket.Status.ToString();
            Comments = new List<Comment>();
        }
    }
}