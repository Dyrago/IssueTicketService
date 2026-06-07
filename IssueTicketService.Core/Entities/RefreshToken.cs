namespace IssueTicketService.Core.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string? TokenValue { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokeDate { get; set; }
        public Guid UserId;
        public User User;
    }
}