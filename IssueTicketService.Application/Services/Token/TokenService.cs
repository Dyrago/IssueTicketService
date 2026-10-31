using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Dto.Token;
using IssueTicketService.Application.Services.User;
using IssueTicketService.Core.Entities;
using IssueTicketService.Core.Repositories;
using Microsoft.Extensions.Configuration;

namespace IssueTicketService.Application.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly ITokenProvider _tokenProvider;
        private readonly IUserService _userService;
        private readonly ITokenRepository _tokenRepository;
        private readonly IConfiguration _config;
        

        public TokenService(
            ITokenProvider tokenProvider,
            IUserService userService,
            ITokenRepository tokenRepository,
            IConfiguration configuration)
        {
            _tokenProvider = tokenProvider;
            _userService = userService;
            _tokenRepository = tokenRepository;
            _config = configuration;
        }

        public async Task<TokenResult> Create(Core.Entities.User user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in user.Roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role.Name.ToString()));
            }

            var authToken = _tokenProvider.GenerateToken(new ClaimsIdentity(userClaims));

            var refreshToken = _tokenProvider.GenerateRefreshToken();
            
            await _tokenRepository.SaveRefreshToken(new Core.Entities.RefreshToken()
            {
               CreateDate = DateTime.UtcNow,
               Expires = DateTime.UtcNow.AddDays(_config.GetValue<int>("Jwt:RefreshLifetimeInDays")),
               TokenValue = RefreshToken.HashTokenValue(refreshToken),
               UserId = user.Id
            });

            return new TokenResult()
            {
                AuthToken = authToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<TokenResult> Refresh(string refreshToken)
        {
            var userRefreshToken = await _tokenRepository.GetRefreshToken(RefreshToken.HashTokenValue(refreshToken));

            if(userRefreshToken == null)
                throw new Exception("Token not found");

            var user = await _userService.GetById(userRefreshToken.UserId);

            if (user == null)
                throw new Exception("User not found.");

            await _tokenRepository.Revoke(userRefreshToken, user.Id);

            var newRefreshToken = _tokenProvider.GenerateRefreshToken();

            await _tokenRepository.SaveRefreshToken(new Core.Entities.RefreshToken()
            {
               CreateDate = DateTime.UtcNow,
               Expires = DateTime.UtcNow.AddDays(_config.GetValue<int>("Jwt:RefreshLifetimeInDays")),
               TokenValue = RefreshToken.HashTokenValue(newRefreshToken),
               UserId = user.Id
            });

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in user.Roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role.Name.ToString()));
            }

            var newAccessToken =_tokenProvider.GenerateToken(new ClaimsIdentity(userClaims));

            return new TokenResult()
            {
              AuthToken = newAccessToken,
              RefreshToken = newRefreshToken  
            };
        }
    }
}