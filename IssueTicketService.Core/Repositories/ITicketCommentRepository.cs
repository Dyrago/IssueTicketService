namespace IssueTicketService.Core.Repositories
{
    public interface ITicketCommentRepository
    {
        Task Create(Core.Entities.Comment comment);
    }
}