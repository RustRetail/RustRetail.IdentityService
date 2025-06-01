using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;
using RustRetail.IdentityService.Domain.Enums;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        const string TableName = "UserProfiles";

        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Gender)
                .HasConversion(
                    gender => gender!.Value,
                    value => Gender.FromValue<Gender>(value)
                );
        }
    }
}
