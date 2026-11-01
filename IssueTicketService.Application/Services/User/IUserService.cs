using IssueTicketService.Application.Dto.Authentication;

namespace IssueTicketService.Application.Services.User
{
    public interface IUserService
    {
        Task<Core.Entities.User?> GetById(Guid id);
        Task<Core.Entities.User?> GetByEmail(string email);
        Task<Guid> Create(Core.Entities.User user);
        Task Update(Guid id, Core.Entities.User user);
        Task Delete(Guid id);
    }
}