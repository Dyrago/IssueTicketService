namespace IssueTicketService.Application.Dto.Ticket
{
    public class TicketCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserEmail { get; set; }
    }
}