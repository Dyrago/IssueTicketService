namespace IssueTicketService.Application.Dto.Token
{
    public class TokenResult
    {
        public string? AuthToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}