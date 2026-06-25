using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Dto.Token;

namespace IssueTicketService.Application.Services.Token
{
    public interface ITokenService
    {
        Task<TokenResult> Create(Core.Entities.User user);
        Task<TokenResult> Refresh(RefreshDataDto dto);
    }
}