using IssueTicketService.Application.Dto.Account;

namespace IssueTicketService.Application.Services.Account
{
    public interface IAccountService
    {
         public Task ChangePassword(ChangePasswordDto changePasswordDto, Guid changeUserId);
         public Task Delete(Guid authorisedUserId, Guid userId);
    }
}