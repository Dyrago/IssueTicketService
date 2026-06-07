using System.Threading.Tasks;
using IssueTicketService.Application.Dto.Authentication;
using IssueTicketService.Application.Dto.Token;
using IssueTicketService.Application.Services.Token;
using IssueTicketService.Application.Services.User;
using IssueTicketService.Application.Services.UserRole;
using IssueTicketService.Core.Enums;

namespace IssueTicketService.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public AuthenticationService(
            ITokenService tokenService,
            IUserService userService,
            IUserRoleService userRoleService
        )
        {
            _tokenService = tokenService;
            _userService = userService;
            _userRoleService = userRoleService;
        }

        public async Task<TokenResult?> Login(LoginDto login)
        {
            var user = await _userService.GetByEmail(login.Email);

            if (user == null)
                return null;

            return _tokenService.Create(user);
        }

        public async Task<Guid?> Register(RegisterUserDto register)
        {
            if (register == null)
                return null;

            if (register.Password != register.RepeatPassword)
                return null;

            var passwordResult = PasswordService.PasswordService.HashPassword(register.Password);

            if (passwordResult == null ||
                passwordResult.Password == null ||
                passwordResult.Salt == null)
                return null;

            var newUserId = await _userService.Create(new Core.Entities.User()
            {
                Email = register.Email,
                Password = passwordResult.Password,
                Salt = passwordResult.Salt
            });

            await _userRoleService.AddRoleToUser(newUserId, (int)RoleEnum.User); 

            return newUserId;
        }
    }
}