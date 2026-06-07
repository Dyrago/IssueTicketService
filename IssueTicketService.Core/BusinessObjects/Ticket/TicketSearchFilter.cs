using System.ComponentModel.DataAnnotations;
using IssueTicketService.Core.Enums;

namespace IssueTicketService.Core.BusinessObjects
{
    public class TicketSearchFilter
    {
        [MaxLength(50)]
        public string? Contains { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Guid? ResolverId { get; set; }
        public Guid? ReporterId { get; set; }
        public int? TicketStatusId { get; set; }
    }
}