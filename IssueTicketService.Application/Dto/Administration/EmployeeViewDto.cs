namespace IssueTicketService.Application.Dto.Administration
{
    public class EmployeeViewDto
    {
        public string Email { get; set; }
        public int AssignedTickets { get; set; }
        public int OpenTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int ClosedTickets { get; set; }
    }
}