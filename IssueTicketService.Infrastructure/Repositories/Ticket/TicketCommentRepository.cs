using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.Ticket
{
    public class TicketCommentRepository : ITicketCommentRepository
    {
        private readonly IDbContextFactory<IssueTicketServiceDbContext> _contextFactory;

        public TicketCommentRepository(IDbContextFactory<IssueTicketServiceDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Create(Core.Entities.Comment comment)
        {
            var context = await _contextFactory.CreateDbContextAsync();

            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }
    }
}