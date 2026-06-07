using System.Security.Claims;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Services.Administration;
using IssueTicketService.Core.BusinessObjects.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers.Administration
{
    [Authorize(Roles = "Admin")]
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
        
        [HttpPost("Administration/RegisterNewModerator")]
        public async Task<IActionResult> RegisterNewModerator([FromBody] RegisterModeratorDto moderatorRegisterDto)
        {
            try
            {
                await _administrationService.RegisterNewModerator(moderatorRegisterDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost("Administration/Employees")]
        public async Task<IActionResult> EmployeesSearch(EmployeeSearchFilter filter)
        {
            try
            {
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();
                    
                var employees = await _administrationService.EmployeeSearch(filter);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}