using IssueTicketService.Core.Entities;
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

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(int id, Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetAll(int ticketId, int page, int size)
        {
            var context = await _contextFactory.CreateDbContextAsync();

            var comments = await context.Comments
            .Where(c => c.TicketId == ticketId)
            .OrderByDescending(c => c.Id)
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

            return comments;
        }

        public async Task<Comment?> Get(int id)
        {
            var context = await _contextFactory.CreateDbContextAsync();

            return await context.Comments.Where(c => c.Id == id)
                                         .FirstOrDefaultAsync();
        }
    }
}