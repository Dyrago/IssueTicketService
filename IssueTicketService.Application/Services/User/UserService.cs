
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Create(Core.Entities.User user)
        {
            return await _userRepository.Create(user);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Core.Entities.User?> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<Core.Entities.User?> GetById(Guid id)
        {
            return await _userRepository.GetById(id);
        }

        public Task Update(int id, Core.Entities.User ticket)
        {
            throw new NotImplementedException();
        }
    }
}