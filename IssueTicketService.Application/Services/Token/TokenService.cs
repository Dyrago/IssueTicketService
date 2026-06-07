using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
using IssueTicketService.Application.Dto.Token;
using IssueTicketService.Application.Services.User;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserService _userService;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(
            ITokenProvider tokenProvider,
            IUserService userService,
            ITokenRepository tokenRepository)
        {
            _tokenProvider = tokenProvider;
            _userService = userService;
            _tokenRepository = tokenRepository;
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
            
            _tokenRepository.SaveRefreshToken(new Core.Entities.RefreshToken()
            {
               CreateDate = DateTime.Now,
               Expires = DateTime.Now.AddDays(7),
               TokenValue = SHA256.HashData(Encoding.UTF8.GetBytes(refreshToken)).ToString(),
               UserId = user.Id
            });

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