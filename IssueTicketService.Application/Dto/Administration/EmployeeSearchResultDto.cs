namespace IssueTicketService.Application.Dto.Administration
{
    public class EmployeeSearchResultDto
    {
        public List<EmployeeDto> Employees { get; set; }
        public int TotalRowsCount { get; set; }
    }
}