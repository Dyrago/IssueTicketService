using IssueTicketService.Core.Repositories;
using IssueTicketService.Infrastructure.Connection;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<IssueTicketServiceDbContext> _contextFactory;
        
        public UserRepository(IDbContextFactory<IssueTicketServiceDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<Guid> Create(Core.Entities.User user)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user.Id; 
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Core.Entities.User?> GetByEmail(string email)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            var user = await dbContext.Users.Include(u => u.Roles)
                                            .Where(u => u.Email == email)
                                            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<Core.Entities.User?> GetById(Guid id)
        {
            using var dbContext = await _contextFactory.CreateDbContextAsync();

            var user = await dbContext.Users.Include(u => u.Roles)
                                            .Where(u => u.Id == id)
                                            .FirstOrDefaultAsync();

            return user;
        }

        public Task Update(int id, Core.Entities.User ticket)
        {
            throw new NotImplementedException();
        }
    }
}