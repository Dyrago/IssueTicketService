using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Dto.Token;

namespace IssueTicketService.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<TokenResult?> Login(LoginDto login);
        Task<Guid?> Register(RegisterUserDto registerDto);
        Task<TokenResult> RefreshAccess(RefreshDataDto dto);
    }
}