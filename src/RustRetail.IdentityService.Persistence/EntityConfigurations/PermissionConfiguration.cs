using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        const string TableName = "Permission";

        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable(TableName);
        }
    }
}
