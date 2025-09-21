using System.Security.Cryptography;
using System.Text;
using IssueTicketService.Application.Dto.Authentication;

namespace IssueTicketService.Application.Services.PasswordService
{
    public static class PasswordService
    {
        private const int _saltSize = 128 / 8;
        private const int _keySize = 256 / 8;
        private const int _iteration = 100000;
        private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;

        public static PasswordResult? HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return null;

            var salt = RandomNumberGenerator.GetBytes(_saltSize);
            var hashedPassword = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password),
                                                        salt, _iteration, hashAlgorithmName, _keySize);

            return new PasswordResult()
            {
                Password = Convert.ToBase64String(hashedPassword),
                Salt = Convert.ToBase64String(salt)
            };
        }

        public static bool VerifyPassword(string inputPassword, string userPassword, string userSalt)
        {
            var haseToCompare = Rfc2898DeriveBytes.Pbkdf2(inputPassword, Encoding.UTF8.GetBytes(userSalt),
                    _iteration, hashAlgorithmName, _keySize);

                return CryptographicOperations.FixedTimeEquals(haseToCompare, Convert.FromBase64String(userPassword));
        }
    }
}