using IssueTicketService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueTicketService.Infrastructure.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany<Comment>(t => t.Comments)
                    .WithOne(c => c.Ticket)
                    .HasForeignKey(c => c.TicketId);

            builder.HasOne(t => t.Reporter)
                    .WithMany(u => u.ReportedTickets)
                    .HasForeignKey(t => t.ReporterId);

            builder.HasOne(t => t.Resolver)
                    .WithMany(u => u.ResolvedTickets)
                    .HasForeignKey(t => t.ResolverId);
        }
    }
}