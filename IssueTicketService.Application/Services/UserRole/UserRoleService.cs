
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.UserRole
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(
            IUserRoleRepository userRoleRepository
        )
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task AddRoleToUser(Guid userId, int roleId)
        {
            await _userRoleRepository.AddRoleToUser(userId, roleId);
        }

        public Task RemoveRoleFromUser(Guid Id, int roleId)
        {
            throw new NotImplementedException();
        }
    }
}