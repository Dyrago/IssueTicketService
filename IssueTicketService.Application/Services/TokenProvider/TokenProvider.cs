using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
namespace IssueTicketService.Application.Services.Token
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IConfiguration _config;
        public TokenProvider(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateToken(ClaimsIdentity userClaims)
        {
            string secretKey = _config["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = userClaims,
                Expires = DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpirationInMinutes")),
                SigningCredentials = credentials,
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };
            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        public async Task<ClaimsPrincipal> GetClaimsFromExpiredToken(string accessToken)
        {
            var handler = new JsonWebTokenHandler();

            string secretKey = _config["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var validationParams = new TokenValidationParameters()
            {
                ValidateIssuer = false, 
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
 
                ValidateLifetime = false
            };
            
            var validToken = await handler.ValidateTokenAsync(accessToken, validationParams);

            if (!validToken.IsValid)
                throw new Exception("Invalid token.");

            var jwt = validToken.SecurityToken as JsonWebToken;

            if (jwt == null || !string.Equals(jwt.Alg, SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new Exception("Invalid token.");

            return new ClaimsPrincipal(validToken.ClaimsIdentity);
        }
    }
}