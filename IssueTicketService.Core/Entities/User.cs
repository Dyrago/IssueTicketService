namespace IssueTicketService.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public List<Role> Roles { get; set; }
        public List<Ticket> ReportedTickets { get; set; }
        public List<Ticket> ResolvedTickets { get; set; }
        public List<Comment> Comments { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }

        public User()
        {
            Roles = new List<Role>();
            ReportedTickets = new List<Ticket>();
            ResolvedTickets = new List<Ticket>();
            RefreshTokens = new List<RefreshToken>();
            Comments = new List<Comment>();
        }
    }
}