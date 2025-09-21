using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueTicketService.Api.Controllers
{
    [Authorize]
    public class AuthorizedController : Controller
    {

    }
}