using System.ComponentModel.DataAnnotations;

namespace IssueTicketService.Application.Dto.Authentication
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required]
        [MinLength(8)]
        [MaxLength(40)]
        public string Password { get; set; }
    }
}