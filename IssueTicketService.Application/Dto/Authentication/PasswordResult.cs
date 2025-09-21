namespace IssueTicketService.Application.Dto.Authentication
{
    public class PasswordResult
    {
        public string? Password { get; set; }
        public string? Salt { get; set; }
    }
}