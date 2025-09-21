using IssueTicketService.Application.Dto.Token;

namespace IssueTicketService.Application.Services.Token
{
    public interface ITokenService
    {
        TokenResult Create(Core.Entities.User user);
        TokenResult Refresh();
    }
}