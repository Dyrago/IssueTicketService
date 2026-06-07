using IssueTicketService.Application.Dto.Administration;
using IssueTicketService.Core.BusinessObjects.Employee;

namespace IssueTicketService.Application.Repositories.Administration
{
    public interface IAdministrationRepository
    {
        public Task<(IEnumerable<EmployeeDto>, int)> EmployeeSearch(EmployeeSearchFilter filter);
        public Task<EmployeeViewDto> GetEmployee(Guid userId);
    }
}