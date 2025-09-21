using IssueTicketService.Core.Enums;

namespace IssueTicketService.Core.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public List<Comment> Comments { get; set; }

        public Guid ReporterId { get; set; }
        public User Reporter { get; set; }

        public Guid? ResolverId { get; set; }
        public User? Resolver { get; set; }

        public TicketStatusEnum Status { get; set; } = TicketStatusEnum.New;

        public Ticket()
        {
            Comments = new List<Comment>();
        }
    }
}