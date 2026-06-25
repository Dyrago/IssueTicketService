namespace IssueTicketService.Application.Dto.Authentication
{
    public class RefreshDataDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}