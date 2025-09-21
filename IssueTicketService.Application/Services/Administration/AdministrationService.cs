using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Core.Enums;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Administration
{
    public class AdministrationService : IAdministrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public AdministrationService(IUserRepository userRepository,
        IUserRoleRepository userRoleRepository)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
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