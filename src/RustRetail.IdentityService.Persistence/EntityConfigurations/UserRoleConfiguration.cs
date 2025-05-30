using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        const string TableName = "UserRole";

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(TableName);
        }
    }
}
