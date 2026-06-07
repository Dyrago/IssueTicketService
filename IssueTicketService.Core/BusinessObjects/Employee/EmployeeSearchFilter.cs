namespace IssueTicketService.Core.BusinessObjects.Employee
{
    public class EmployeeSearchFilter
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
    }
}