using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        const string TableName = "Roles";

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(TableName);
        }
    }
}
