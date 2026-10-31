namespace IssueTicketService.Application.Dto.Authentication
{
    public class CurrentDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public List<string> roles { get; set; }
    }
}