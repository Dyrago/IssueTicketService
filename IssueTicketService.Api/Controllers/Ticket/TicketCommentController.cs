using System.Net;
using System.Security.Claims;
using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Application.Services.Ticket;
using IssueTicketService.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers.Ticket
{
    public class TicketCommentController : AuthorizedController
    {
        private readonly ILogger<TicketCommentController> _logger;
        private readonly ITicketCommentService _ticketCommentService;

        public TicketCommentController(
            ILogger<TicketCommentController> logger,
            ITicketCommentService ticketCommentService)
        {
            _logger = logger;
            _ticketCommentService = ticketCommentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TicketCommentCreateDto comment)
        {
            try
            {
                var userGuid = User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First().Value;

                if (userGuid == null)
                    return Unauthorized();

                await _ticketCommentService.Create(comment, new Guid(userGuid));

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}