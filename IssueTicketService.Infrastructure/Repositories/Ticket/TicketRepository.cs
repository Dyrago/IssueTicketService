using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.Ticket
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IDbContextFactory<IssueTicketServiceDbContext> _contextFactory;

        public TicketRepository(IDbContextFactory<IssueTicketServiceDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Create(Core.Entities.Ticket ticket)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Tickets.AddAsync(ticket);

            await context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Tickets.Where(t => t.Id == id).ExecuteDeleteAsync();

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Core.Entities.Ticket>> Get()
        {
            using var context = _contextFactory.CreateDbContext();
            var tickets = await context.Tickets.ToListAsync();

            return tickets;
        }

        public async Task<Core.Entities.Ticket?> GetById(int id)
        {
            using var context = _contextFactory.CreateDbContext();
            var ticket = await context.Tickets.Where(t => t.Id == id).FirstOrDefaultAsync();

            return ticket;
        }

        public async Task<IEnumerable<Core.Entities.Ticket?>> GetByUserId(Guid userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var tickets = await context.Tickets.Where(t => t.ReporterId == userId).ToListAsync();

            return tickets;
        }

        public async Task Update(int id, Core.Entities.Ticket ticket)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Tickets.Where(t => t.Id == id)
                                 .ExecuteUpdateAsync(t => t.SetProperty(t => t.Title, ticket.Title)
                                                                                  .SetProperty(t => t.Content, ticket.Content));

            await context.SaveChangesAsync();
        }

        public async Task ChangeStatus(int taskId, int statusId)
        {
            using var context = _contextFactory.CreateDbContext();

            var ticket = await context.Tickets.Where(t => t.Id == taskId)
                                              .ExecuteUpdateAsync(t => t.SetProperty(t => t.Status, (Core.Enums.TicketStatusEnum)statusId));

            await context.SaveChangesAsync();
        }
    }
}