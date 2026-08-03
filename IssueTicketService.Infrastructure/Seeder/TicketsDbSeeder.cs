using IssueTicketService.Application.Services.PasswordService;
using IssueTicketService.Core.Entities;
using IssueTicketService.Core.Enums;
using IssueTicketService.Infrastructure.Connection;

namespace IssueTicketService.Infrastructure.Seeder
{
    public static class TicketsDbSeeder
    {
        public static void Seed(IssueTicketServiceDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                var adminPasswordResult = PasswordService.HashPassword("Admin!123");
                
                var admin = new User()
                {
                    Email = "admin@tickets.com",
                    Password = adminPasswordResult.Password,
                    Salt = adminPasswordResult.Salt,
                    CreateDate = DateTime.UtcNow,
                    Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Id = (int)RoleEnum.Admin,
                            Name = "Admin",
                            Description = string.Empty
                        }
                    }
                };

                dbContext.Users.Add(admin);

                var modPasswordResult = PasswordService.HashPassword("Moderator!123");
                
                var mod = new User()
                {
                    Email = "mod@tickets.com",
                    Password = modPasswordResult.Password,
                    Salt = modPasswordResult.Salt,
                    CreateDate = DateTime.UtcNow,
                    Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Id = (int)RoleEnum.Moderator,
                            Name = "Mod",
                            Description = string.Empty
                        }
                    }
                };

                dbContext.Users.Add(mod);

                var userPasswordResult = PasswordService.HashPassword("User!123");
                
                var user = new User()
                {
                    Email = "user@tickets.com",
                    Password = userPasswordResult.Password,
                    Salt = userPasswordResult.Salt,
                    CreateDate = DateTime.UtcNow,
                    Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Id = (int)RoleEnum.User,
                            Name = "User",
                            Description = string.Empty
                        }
                    }
                };

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }
    }
}