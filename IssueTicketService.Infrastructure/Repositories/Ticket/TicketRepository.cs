using IssueTicketService.Core.BusinessObjects;
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

        public async Task Delete(int id, Guid? userId = null)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Tickets.Where(t => t.Id == id && t.ReporterId == userId).ExecuteDeleteAsync();

            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Core.Entities.Ticket>> Search(TicketSearchFilter filter)
        {
            using var context = _contextFactory.CreateDbContext();

            var query = context.Tickets.AsQueryable();

            if (filter.ReporterId.HasValue)
                query = query.Where(t => t.ReporterId == filter.ReporterId);

            if (filter.ResolverId.HasValue)
                query = query.Where(t => t.ResolverId == filter.ResolverId || t.ResolverId == null);

            if (filter.TicketStatusId.HasValue)
                query = query.Where(t => Convert.ToInt32(t.Status) == filter.TicketStatusId);

            if (!string.IsNullOrEmpty(filter.Contains))
                query = query.Where(t => t.Content.Contains(filter.Contains) || t.Title.Contains(filter.Contains));

            if (filter.DateFrom.HasValue)
                query = query.Where(t => t.CreateDate >= filter.DateFrom);          

            if (filter.DateTo.HasValue)
                query = query.Where(t => t.CreateDate <= filter.DateTo);          


            var tickets = await query.ToListAsync();

            return tickets;
        }

        public async Task<Core.Entities.Ticket?> GetById(int id, Guid? userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var ticket = await context.Tickets.Where(t => t.Id == id && t.ReporterId == userId).FirstOrDefaultAsync();

            return ticket;
        }

        public async Task<IEnumerable<Core.Entities.Ticket?>> GetByUserId(Guid userId)
        {
            using var context = _contextFactory.CreateDbContext();
            var tickets = await context.Tickets.Where(t => t.ReporterId == userId).ToListAsync();

            return tickets;
        }

        public async Task Update(int id, Core.Entities.Ticket ticket, Guid? userId = null)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Tickets.Where(t => t.Id == id && t.ReporterId == userId)
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