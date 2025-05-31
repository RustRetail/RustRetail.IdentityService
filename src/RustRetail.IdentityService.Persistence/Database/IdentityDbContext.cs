using Microsoft.EntityFrameworkCore;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.SharedKernel.Domain.Models;
using RustRetail.SharedPersistence.Abstraction;

namespace RustRetail.IdentityService.Persistence.Database
{
    public class IdentityDbContext : DbContext, IHasOutboxMessage
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<OutboxMessage> OutboxMessage { get; set; }

        public IdentityDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityDbContext).Assembly);
        }
    }
}
