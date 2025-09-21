using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Services.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers.Administration
{
    public class AdministrationController : AuthorizedController
    {
        private readonly ILogger<AdministrationController> _logger;
        private readonly IAdministrationService _administrationService;

        public AdministrationController(ILogger<AdministrationController> logger,
        IAdministrationService administrationService)
        {
            _logger = logger;
            _administrationService = administrationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Administration/RegisterNewModerator")]
        public async Task<IActionResult> RegisterNewModerator([FromBody]RegisterModeratorDto moderatorRegisterDto)
        {
            try
            {
                await _administrationService.RegisterNewModerator(moderatorRegisterDto);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}