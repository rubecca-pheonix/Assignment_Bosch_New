using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment_Bosch.Integration.Configurations
{
	public class UserInfoConfigurations : IEntityTypeConfiguration<UserInfo>
    {
        public void Configure(EntityTypeBuilder<UserInfo> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__UserInfos");


            entity.Property(e => e.Username).HasMaxLength(30);
            entity.Property(e => e.Password).HasMaxLength(30);
        }
    }
}
