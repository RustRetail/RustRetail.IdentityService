using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        const string TableName = "Users";

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(TableName);
            
        }
    }
}
