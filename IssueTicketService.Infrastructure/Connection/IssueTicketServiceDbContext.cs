using System.Reflection;
using IssueTicketService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IssueTicketService.Infrastructure.Connection
{
    public class IssueTicketServiceDbContext : DbContext
    {
        public IssueTicketServiceDbContext()
        {

        }
        public IssueTicketServiceDbContext(DbContextOptions<IssueTicketServiceDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}