using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        const string TableName = "UserClaims";

        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable(TableName);

            builder.HasIndex(x => new { x.UserId, x.Type })
                .IsUnique();
        }
    }
}
