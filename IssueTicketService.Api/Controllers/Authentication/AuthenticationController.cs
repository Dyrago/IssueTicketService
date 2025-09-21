using System.Net;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers.Authentication
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        [HttpPost("Authentication/Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            try
            {
                var token = await _authenticationService.Login(login);

                if (token == null)
                    return StatusCode(500);

                Response.Cookies.Append("RfsTkn", token.RefreshToken, new CookieOptions()
                {
                    SameSite = SameSiteMode.Strict,
                    HttpOnly = true,
                    Secure = true,
                    Expires = DateTime.Now.AddHours(1)
                });

                return new JsonResult(token.AuthToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Authentication/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto register)
        {
            try
            {
                var newUserId = await _authenticationService.Register(register);
                return new JsonResult(newUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}