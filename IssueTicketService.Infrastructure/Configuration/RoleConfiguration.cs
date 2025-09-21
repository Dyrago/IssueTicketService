using IssueTicketService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssueTicketService.Infrastructure.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(r => r.Description)
            .HasMaxLength(1000);

            builder.HasMany(r => r.Users)
                    .WithMany(u => u.Roles);
        }
    }
}