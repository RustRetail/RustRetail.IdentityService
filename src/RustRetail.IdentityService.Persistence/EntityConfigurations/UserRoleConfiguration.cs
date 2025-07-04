﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RustRetail.IdentityService.Domain.Entities;

namespace RustRetail.IdentityService.Persistence.EntityConfigurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        const string TableName = "UserRoles";

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
