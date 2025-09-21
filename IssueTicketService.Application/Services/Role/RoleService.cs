
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<Core.Entities.Role> GetById(int id)
        {
            return _roleRepository.GetById(id);
        }

        public Task<Core.Entities.Role> GetByName(string name)
        {
            return _roleRepository.GetByName(name);
        }
    }
}