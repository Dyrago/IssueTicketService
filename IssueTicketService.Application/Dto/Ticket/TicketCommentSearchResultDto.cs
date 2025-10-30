namespace IssueTicketService.Application.Dto.Ticket
{
    public class TicketCommentSearchResultDto
    {
        public IEnumerable<TicketCommentDto> Comments { get; set; } = new List<TicketCommentDto>();
        public int TotalRowsCount { get; set; }
    }
}