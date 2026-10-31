using System.Security.Cryptography;
using System.Text;

namespace IssueTicketService.Core.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string? TokenValue { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime Expires { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? RevokeDate { get; set; }
        public Guid UserId;
        public User User;

        public static string HashTokenValue(string value)
        {
            if(!string.IsNullOrEmpty(value))
                return Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(value)));
            
            return string.Empty;
        }
    }
}