using System.Security.Claims;

namespace IssueTicketService.Application.Services.Token
{
    public interface ITokenProvider
    {
        public string GenerateToken(ClaimsIdentity userClaims);
        public string GenerateRefreshToken();
        public Task<ClaimsPrincipal> GetClaimsFromExpiredToken(string accessToken);
    }
}