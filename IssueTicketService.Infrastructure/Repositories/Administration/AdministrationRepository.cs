using IssueTicketService.Application.Dto.Administration;
using IssueTicketService.Application.Repositories.Administration;
using IssueTicketService.Core.BusinessObjects.Employee;
using IssueTicketService.Core.Enums;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.Administration
{
    public class AdministrationRepository : IAdministrationRepository
    {
        private IDbContextFactory<IssueTicketServiceDbContext> _dbContextFactory;
        public AdministrationRepository(IDbContextFactory<IssueTicketServiceDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<(IEnumerable<EmployeeDto>, int)> EmployeeSearch(EmployeeSearchFilter filter)
        {
            var connection = await _dbContextFactory.CreateDbContextAsync();

            var query = connection.Users.Where(u => u.Roles.Any(r => r.Id == (int)RoleEnum.Moderator));

            var totalRowsCount = await query.CountAsync();

            var employees = await query.OrderByDescending(c => c.Id)
                                       .Skip(filter.Page * filter.Size)
                                       .Take(filter.Size)
                                       .Select(e => new EmployeeDto(){ UserId = e.Id, Email = e.Email })
                                       .ToListAsync();

            return (employees, totalRowsCount);
        }

        public async Task<EmployeeViewDto> GetEmployee(Guid userId)
        {
            var connection = await _dbContextFactory.CreateDbContextAsync();

            var employeeView = await connection.Users
            .Where(u => u.Id == userId)
            .Select(u => new EmployeeViewDto()
            {
                Email = u.Email,
                AssignedTickets = connection.Tickets
                    .Count(t => t.ResolverId == u.Id),

                OpenTickets = connection.Tickets
                    .Count(t => t.ResolverId == u.Id && t.Status == TicketStatusEnum.InProgress),

                ResolvedTickets = connection.Tickets
                    .Count(t => t.ResolverId == u.Id && t.Status == TicketStatusEnum.Resolved),

                ClosedTickets = connection.Tickets
                    .Count(t => t.ResolverId == u.Id && t.Status == TicketStatusEnum.Closed)
            })
            .FirstOrDefaultAsync();

            return employeeView;
        }
    }
}