using System.Security.Claims;
using IssueTicketService.Application.Dto.Ticket;
using IssueTicketService.Application.Services.Ticket;
using IssueTicketService.Core.Entities;
using IssueTicketService.Core.Enums;
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
        public async Task<IActionResult> Search(TicketCommentsSearchFilter filter)
        {
            try
            {
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);
                
                if(userGuid == null)
                    return Unauthorized();

                if (User.IsInRole(RoleEnum.Admin.ToString()))
                    return Ok(await _ticketCommentService.Search(filter, new Guid(userGuid), RoleEnum.Admin));
                
                else if (User.IsInRole(RoleEnum.Moderator.ToString()))
                    return Ok(await _ticketCommentService.Search(filter, new Guid(userGuid), RoleEnum.Moderator));

                else if (User.IsInRole(RoleEnum.User.ToString()))
                    return Ok(await _ticketCommentService.Search(filter, new Guid(userGuid), RoleEnum.User));

                return Forbid();

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
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();
                
                if (User.IsInRole(RoleEnum.Admin.ToString()))
                    await _ticketCommentService.Create(comment, new Guid(userGuid), RoleEnum.Admin);
                
                else if (User.IsInRole(RoleEnum.Moderator.ToString()))
                    await _ticketCommentService.Create(comment, new Guid(userGuid), RoleEnum.Moderator);

                else if (User.IsInRole(RoleEnum.User.ToString()))
                    await _ticketCommentService.Create(comment, new Guid(userGuid), RoleEnum.User);
                else
                    return Forbid();

                return StatusCode(StatusCodes.Status201Created);
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
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();

                if (User.IsInRole(RoleEnum.Admin.ToString()))
                    await _ticketCommentService.Edit(comment, new Guid(userGuid), RoleEnum.Admin);

                if (User.IsInRole(RoleEnum.Moderator.ToString()))
                    await _ticketCommentService.Edit(comment, new Guid(userGuid), RoleEnum.Moderator);

                if (User.IsInRole(RoleEnum.User.ToString()))
                    await _ticketCommentService.Edit(comment, new Guid(userGuid), RoleEnum.User);

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
                var userGuid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userGuid == null)
                    return Unauthorized();

                if (User.IsInRole(RoleEnum.Admin.ToString()))
                    await _ticketCommentService.Delete(commentId, new Guid(userGuid), RoleEnum.Admin);

                if (User.IsInRole(RoleEnum.Moderator.ToString()))
                    await _ticketCommentService.Delete(commentId, new Guid(userGuid), RoleEnum.Moderator);

                if (User.IsInRole(RoleEnum.User.ToString()))
                    await _ticketCommentService.Delete(commentId, new Guid(userGuid), RoleEnum.User);

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