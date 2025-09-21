using System.Security.Claims;
using IssueTicketService.Application.Dto.Token;
using IssueTicketService.Application.Services.User;

namespace IssueTicketService.Application.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserService _userService;

        public TokenService(
            ITokenProvider tokenProvider,
            IUserService userService)
        {
            _tokenProvider = tokenProvider;
            _userService = userService;
        }

        public TokenResult Create(Core.Entities.User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            foreach (var role in user.Roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role.Name.ToString()));
            }

            var authToken = _tokenProvider.GenerateToken(new ClaimsIdentity(userClaims));

            var refreshToken = _tokenProvider.GenerateRefreshToken();
            //save refreshToken to db

            return new TokenResult()
            {
                AuthToken = authToken,
                RefreshToken = refreshToken
            };
        }

        public TokenResult Refresh()
        {
            throw new NotImplementedException();
        }
    }
}