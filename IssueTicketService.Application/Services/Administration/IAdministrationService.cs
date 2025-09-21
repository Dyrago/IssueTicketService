using IssueTicketService.Application.Dto.Authentication;

namespace IssueTicketService.Application.Services.Administration
{
    public interface IAdministrationService
    {
        Task RegisterNewModerator(RegisterModeratorDto moderatorDto);
    }
}