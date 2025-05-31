using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        const string TableName = "UserProfiles";

        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);
        }
    }
}
