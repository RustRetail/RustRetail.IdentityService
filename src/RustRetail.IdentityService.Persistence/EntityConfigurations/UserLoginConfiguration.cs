using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        const string TableName = "UserLogins";

        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.ToTable(TableName);
        }
    }
}
