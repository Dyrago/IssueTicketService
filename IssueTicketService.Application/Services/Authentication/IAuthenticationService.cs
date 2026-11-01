using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Dto.Token;

namespace IssueTicketService.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenResult?> Login(LoginDto login);
        public Task Logout(string refreshToken, Guid userId);
        Task<Guid?> Register(RegisterUserDto registerDto);
        Task<TokenResult> RefreshAccess(string refreshToken);
        Task<CurrentDto?> GetCurrent(Guid id);
    }
}