using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IssueTicketService.Application.Dto.Authentication;
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

        public async Task<TokenResult> Create(Core.Entities.User user)
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
            
            await _tokenRepository.SaveRefreshToken(new Core.Entities.RefreshToken()
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

        public async Task<TokenResult> Refresh(RefreshDataDto dto)
        {
            ClaimsPrincipal principal;
            try
            {
                principal = await _tokenProvider.GetClaimsFromExpiredToken(dto.AccessToken);
            }
            catch (Exception)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
                         principal.FindFirst("sub")?.Value;

            if (userIdClaim == null)
                throw new Exception("User not found.");

            var userId = new Guid(userIdClaim);

            var user = await _userService.GetById(userId);

            if (user == null)
                throw new Exception("User not found.");

            var refreshToken = user.RefreshTokens.FirstOrDefault(rt =>
             rt.IsRevoked == false &&
             rt.TokenValue == SHA256.HashData(Encoding.UTF8.GetBytes(dto.RefreshToken)).ToString());
            
            if (refreshToken == null)
                throw new Exception("Invalid refresh token.");

            await _tokenRepository.Revoke(refreshToken, userId);

            var newRefreshToken = _tokenProvider.GenerateRefreshToken();

            await _tokenRepository.SaveRefreshToken(new Core.Entities.RefreshToken()
            {
               CreateDate = DateTime.Now,
               Expires = DateTime.Now.AddDays(7),
               TokenValue = SHA256.HashData(Encoding.UTF8.GetBytes(newRefreshToken)).ToString(),
               UserId = user.Id
            });

            var newAccessToken =_tokenProvider.GenerateToken(new ClaimsIdentity(principal.Claims));

            return new TokenResult()
            {
              AuthToken = newAccessToken,
              RefreshToken = newRefreshToken  
            };
        }
    }
}