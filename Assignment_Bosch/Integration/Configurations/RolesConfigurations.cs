using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment_Bosch.Integration.Configurations
{
	public class RolesConfigurations : IEntityTypeConfiguration<Roles>
    {
		public void Configure(EntityTypeBuilder<Roles> entity)
        {
            entity.HasKey(e => e.RoleId)
                .HasName("PK__Roles");


            entity.Property(e => e.RoleName).HasMaxLength(30);
        }
    }
}

