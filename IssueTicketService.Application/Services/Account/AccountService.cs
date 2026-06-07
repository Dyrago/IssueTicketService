using System.Security.Permissions;
using IssueTicketService.Application.Dto.Account;
using IssueTicketService.Application.Services.User;
using IssueTicketService.Core.Repositories;

namespace IssueTicketService.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private IUserService _userService;
        private IAccountRepository _accountRepository;

        public AccountService(
            IUserService userService,
            IAccountRepository accountRepository)
        {
            _userService = userService;
            _accountRepository = accountRepository;
        }

        public async Task ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (changePasswordDto.NewPassword == changePasswordDto.OldPassword)
                throw new Exception("New password can not be the same as old password");

            var user = await _userService.GetById(changePasswordDto.UserId);

            if (user == null)
                throw new Exception("Old password is invalid.");

            var correctOldPassword = PasswordService.PasswordService.VerifyPassword(changePasswordDto.OldPassword, user.Password, user.Salt);

            if (!correctOldPassword)
                throw new Exception("Old password is invalid.");

            var passwordResult = PasswordService.PasswordService.HashPassword(changePasswordDto.NewPassword);

            if (passwordResult == null)
                throw new Exception("Error during operation.");

            await _accountRepository.ChangePassword(changePasswordDto.UserId, passwordResult.Password, passwordResult.Salt);
        }

        public async Task Delete(Guid authorizedUserId, Guid userId)
        {
            if (authorizedUserId != userId)
                throw new Exception("Operation unaviable.");

            await _accountRepository.Delete(userId);
        }
    }
}