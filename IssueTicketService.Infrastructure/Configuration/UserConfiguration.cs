using IssueTicketService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueTicketService.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
              public void Configure(EntityTypeBuilder<User> builder)
              {
                     builder.HasKey(user => user.Id);

                     builder.Property(p => p.Email)
                            .IsRequired();

                     builder.Property(p => p.Password)
                            .IsRequired();

                     builder.Property(p => p.Salt)
                             .IsRequired();

                     //N <-> N relation
                     builder.HasMany(user => user.Roles)
                            .WithMany(role => role.Users);

                     builder.HasMany(u => u.ReportedTickets)
                            .WithOne(t => t.Reporter)
                            .HasForeignKey(t => t.ReporterId)
                            .OnDelete(DeleteBehavior.Restrict);

                     builder.HasMany(u => u.ResolvedTickets)
                            .WithOne(t => t.Resolver)
                            .HasForeignKey(t => t.ResolverId)
                            .OnDelete(DeleteBehavior.Restrict);

                     builder.HasMany(u => u.Comments)
                            .WithOne(c => c.User)
                            .HasForeignKey(c => c.UserId);
        }
    }
}