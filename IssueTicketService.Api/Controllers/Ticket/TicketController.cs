using System.Security.Claims;
using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Application.Services.Ticket;
using IssueTicketService.Core.BusinessObjects;
using IssueTicketService.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers
{
    [Authorize(Roles = "Admin,Mod,User")]
    public class TicketController : AuthorizedController
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(
            ITicketService ticketService,
            ILogger<TicketController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        [HttpGet("Tickets")]
        public async Task<IActionResult> Search(TicketSearchFilter filter)
        {
            try
            {
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();

                if (User.IsInRole(RoleEnum.Admin.ToString()))
                {
                   return Ok(await _ticketService.Search(filter));
                }

                if (User.IsInRole(RoleEnum.Moderator.ToString()))
                {
                    filter.ResolverId = new Guid(userGuid);

                    return Ok(await _ticketService.Search(filter));
                }

                if (!User.IsInRole(RoleEnum.Admin.ToString()) || !User.IsInRole(RoleEnum.Moderator.ToString()))
                    filter.ReporterId = new Guid(userGuid);

                return Ok(await _ticketService.Search(filter));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("Ticket/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();

                if(User.IsInRole(RoleEnum.Admin.ToString()) || User.IsInRole(RoleEnum.Moderator.ToString()))
                {
                    var ticket = await _ticketService.GetById(id);
                    return ticket != null ? Ok(ticket) : NotFound();
                }
                else
                {
                    var ticket = await _ticketService.GetById(id, new Guid(userGuid));
                    return ticket != null ? Ok(ticket) : NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [Authorize(Roles = "Admin,Mod")]
        [HttpPost("Ticket/{id}/ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] int statusId)
        {
            try
            {
                await _ticketService.ChangeStatus(id, statusId);

                return Ok(new { Data = "Status changed successfuly." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [Authorize(Roles = "Admin,Mod")]
        [HttpGet("Tickets/User/{guid}")]
        public async Task<IActionResult> GetById(string guid)
        {
            try
            {
                return Ok(await _ticketService.GetByUserId(new Guid(guid)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPut("Ticket/Edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TicketRequestDto ticket)
        {
            try
            {
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();

                if(User.IsInRole(RoleEnum.Admin.ToString()) || User.IsInRole(RoleEnum.Moderator.ToString()))
                    await _ticketService.Update(id, ticket);
                else
                    await _ticketService.Update(id, ticket, new Guid(userGuid));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("Ticket/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                 var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();

                if(User.IsInRole(RoleEnum.Admin.ToString()) || User.IsInRole(RoleEnum.Moderator.ToString()))
                    await _ticketService.Delete(id);
                else
                    await _ticketService.Delete(id, new Guid(userGuid));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("Ticket/Create")]
        public async Task<IActionResult> Create([FromBody] TicketRequestDto ticket)
        {
            try
            {
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();

                await _ticketService.Create(ticket, new Guid(userGuid));
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