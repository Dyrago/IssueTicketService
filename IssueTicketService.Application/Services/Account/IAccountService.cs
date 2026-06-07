using IssueTicketService.Application.Dto.Account;

namespace IssueTicketService.Application.Services.Account
{
    public interface IAccountService
    {
         public Task ChangePassword(ChangePasswordDto changePasswordDto);
         public Task Delete(Guid authorisedUserId, Guid userId);
    }
}