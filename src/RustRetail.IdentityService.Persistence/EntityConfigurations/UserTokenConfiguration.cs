using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        const string TableName = "UserTokens";

        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable(TableName);

            builder.HasIndex(x => new { x.UserId, x.Provider, x.Name })
                .IsUnique();
        }
    }
}
