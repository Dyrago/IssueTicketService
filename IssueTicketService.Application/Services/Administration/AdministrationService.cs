using IssueTicketService.Application.Dto.Administration;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Repositories.Administration;
using IssueTicketService.Core.BusinessObjects.Employee;
using IssueTicketService.Core.Enums;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IAdministrationRepository _administreationRepository;

        public AdministrationService(IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IAdministrationRepository administrationRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _administreationRepository = administrationRepository;
        }

        public async Task<EmployeeSearchResultDto> EmployeeSearch(EmployeeSearchFilter filter)
        {
            var employees = Enumerable.Empty<EmployeeDto>();
            var totalRowsCount = 0;

            (employees, totalRowsCount) = await _administreationRepository.EmployeeSearch(filter);

            return new EmployeeSearchResultDto()
            {
              Employees = employees.ToList(),
              TotalRowsCount = totalRowsCount  
            };
        }

        public async Task<EmployeeViewDto> GetEmployee(Guid userId)
        {
            var employee = await _administreationRepository.GetEmployee(userId);

            return employee;
        }

        public async Task RegisterNewModerator(RegisterModeratorDto moderatorDto)
        {
            var passwordResult = PasswordService.PasswordService.HashPassword(moderatorDto.Password);

            var user = new Core.Entities.User()
            {
                Email = moderatorDto.Email,
                Password = passwordResult.Password,
                Salt = passwordResult.Salt
            };

            var newUserGuid = await _userRepository.Create(user);

            await _userRoleRepository.AddRoleToUser(newUserGuid, (int)RoleEnum.Moderator);
        }
    }
}