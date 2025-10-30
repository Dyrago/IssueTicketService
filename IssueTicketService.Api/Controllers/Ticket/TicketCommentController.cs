using System.Security.Claims;
using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Application.Services.Ticket;
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

        [HttpGet("Ticket/{ticketId}/Comments")]
        public async Task<IActionResult> Search(int ticketId, int page = 0, int size = 10)
        {
            try
            {
                return new JsonResult(await _ticketCommentService.Search(ticketId, page, size));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpPost("TicketComment/Create")]
        public async Task<IActionResult> Create([FromBody]TicketCommentCreateDto comment)
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

        [HttpPut("TicketComment/Edit")]
        public async Task<IActionResult> Edit([FromBody] TicketCommentEditDto comment)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpDelete("TicketComment/{commentId}/Delete")]
        public async Task<IActionResult> Delete(int commentId)
        {
            try
            {
                await _ticketCommentService.Delete(commentId);
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