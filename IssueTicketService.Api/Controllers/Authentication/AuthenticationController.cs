using System.Security.Claims;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Services.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers.Authentication
{
    public class AuthenticationController : ControllerBase
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
                    return StatusCode(400);

                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Authentication/Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {   
                var refreshTokenCookieValue = Request.Cookies.FirstOrDefault(c => c.Key == "RfsTkn").Value;

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(string.IsNullOrEmpty(userId))
                    return StatusCode(StatusCodes.Status400BadRequest);

                await _authenticationService.Logout(refreshTokenCookieValue, new Guid(userId));

                Response.Cookies.Delete("RfsTkn");
                Response.Cookies.Delete("authJwt");

                return Ok();
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
                return Ok(newUserId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Authentication/Refresh")]
        public async Task<IActionResult> Refresh([FromBody]string refreshToken)
        {
            try
            {
                if (string.IsNullOrEmpty(refreshToken))
                {
                    return Unauthorized("Refresh token is missing.");
                }
                
                var result = await _authenticationService.RefreshAccess(refreshToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("Authentication/Current")]
        public async Task<IActionResult> GetCurrent()
        {
            try
            {
                var userId = Request.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(userId))
                    return Unauthorized();
                
                return Ok(await _authenticationService.GetCurrent(new Guid(userId)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}