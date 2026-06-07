using IssueTicketService.Application.Dto.Administration;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Core.BusinessObjects.Employee;

namespace IssueTicketService.Application.Services.Administration
{
    public interface IAdministrationService
    {
        Task RegisterNewModerator(RegisterModeratorDto moderatorDto);
        Task<EmployeeSearchResultDto> EmployeeSearch(EmployeeSearchFilter filter);
        Task<EmployeeViewDto> GetEmployee(Guid userId);
    }
}