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

        public async Task Delete(int id)
        {
            var context = await _contextFactory.CreateDbContextAsync();

            await context.Comments.Where(c => c.Id == id).ExecuteDeleteAsync();
            await context.SaveChangesAsync();
        }

        public async Task Edit(int id, string content)
        {
            var context = await _contextFactory.CreateDbContextAsync();

            await context.Comments.Where(c => c.Id == id)
                                  .ExecuteUpdateAsync(c => c.SetProperty(c => c.Content, content)); 

            await context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<Comment>, int)> Search(int ticketId, int page, int size)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var query = context.Comments
            .AsNoTracking()
            .Where(c => c.TicketId == ticketId);

            var totalRowsCount = await query.CountAsync();

            var comments = await query
            .Include(c => c.User)
            .OrderByDescending(c => c.Id)
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

            return new (comments, totalRowsCount);
        }

        public async Task<Comment?> Get(int id)
        {
            var context = await _contextFactory.CreateDbContextAsync();

            return await context.Comments.Where(c => c.Id == id)
                                         .FirstOrDefaultAsync();
        }
    }
}