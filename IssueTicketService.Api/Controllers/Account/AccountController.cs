using System.Security.Claims;
using IssueTicketService.Application.Dto.Account;
using IssueTicketService.Application.Services.Account;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers.Account
{
    public class AccountController : AuthorizedController
    {
        private ILogger<AccountController> _logger;
        private IAccountService _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpPost("Account/ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            try
            {
                await _accountService.ChangePassword(changePasswordDto);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("Account/Delete")]
        public async Task<IActionResult> Delete(Guid userId)
        {
            try
            {
                var authUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(string.IsNullOrEmpty(authUserId))
                    return Unauthorized();

                await _accountService.Delete(new Guid(authUserId), userId);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
    }
}