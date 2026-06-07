using System.Net;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
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
                    return StatusCode(500);

                return Ok(new { Data = token});
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
        public async Task<IActionResult> Refresh()
        {
            try
            {
                if (!Request.Cookies.TryGetValue("RfsTkn", out var refreshToken))
                {
                    return Unauthorized("Refresh token is missing.");
                }

                //Call to service that will verify old token and will return new pair of tokens

                //Return new access token and refresh token in response
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }
    }
}