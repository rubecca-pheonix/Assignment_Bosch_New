using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Assignment_Bosch.Integration
{
	public class AuthContext : DbContext
    {
		public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        public virtual DbSet<UserInfo> UserInfos { get; set; }

        public virtual DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.UserInfoConfigurations());

        }
    }
}

