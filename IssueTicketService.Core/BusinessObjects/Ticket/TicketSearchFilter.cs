using System.ComponentModel.DataAnnotations;

namespace IssueTicketService.Core.BusinessObjects
{
    public class TicketSearchFilter
    {
        [MaxLength(50)]
        public string? Contains { get; set; }
        public DateTime? CreateDate { get; set; }
        public Guid? UserId { get; set; } 
    }
}