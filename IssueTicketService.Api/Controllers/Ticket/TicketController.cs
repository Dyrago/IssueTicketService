using System.Security.Claims;
using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Application.Services.Ticket;
using IssueTicketService.Core.BusinessObjects;
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
                var userGuid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)?.FirstOrDefault()?.Value;

                if (userGuid == null)
                    return Unauthorized();

                if (!User.IsInRole("Admin") || !User.IsInRole("Mod"))
                    filter.UserId = new Guid(userGuid);

                return new JsonResult(await _ticketService.Search(filter));
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
                return new JsonResult(await _ticketService.GetById(id));
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

                return new JsonResult(new { Data = "Status changed successfuly." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("Tickets/User/{guid}")]
        public async Task<IActionResult> GetById(string guid)
        {
            try
            {
                return new JsonResult(await _ticketService.GetByUserId(new Guid(guid)));
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
                await _ticketService.Update(id, ticket);
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
                await _ticketService.Delete(id);
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
                var userGuid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

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