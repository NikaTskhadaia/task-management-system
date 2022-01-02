using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using TMS.Domain.Common;
using TMS.Domain.Entities;
using TMS.Infrastructure.Identity;

namespace TMS.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TaskEntity> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
            SeedAdmin(builder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Entity<Guid>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        private static void SeedAdmin(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole() 
                { 
                    Id = "8d552bbc-0c87-4c4d-905a-2b4ea539f40a",
                    Name = "Admin"
                });

            AppUser user = new()
            {
                Id = "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                UserName = "Admin"
            };
            PasswordHasher<AppUser> passwordHasher = new();
            user.PasswordHash = passwordHasher.HashPassword(user, "Admin*123456");
            builder.Entity<AppUser>().HasData(user);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "8d552bbc-0c87-4c4d-905a-2b4ea539f40a",
                    UserId = "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee"
                });

            builder.Entity<IdentityUserClaim<string>>().HasData(
                new IdentityUserClaim<string>
                {
                    Id = 1,
                    UserId = "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                    ClaimType = "Permission",
                    ClaimValue = "Task_Create"
                },
                new IdentityUserClaim<string>
                {
                    Id = 2,
                    UserId = "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                    ClaimType = "Permission",
                    ClaimValue = "Task_Update"
                },
                new IdentityUserClaim<string>
                {
                    Id = 3,
                    UserId = "0afd9d65-9058-4c1e-ac5f-88a4304fa1ee",
                    ClaimType = "Permission",
                    ClaimValue = "Task_Delete"
                });
        }
    }
}
