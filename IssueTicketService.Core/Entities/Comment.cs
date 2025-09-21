namespace IssueTicketService.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}