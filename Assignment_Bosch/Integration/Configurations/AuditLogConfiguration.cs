using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assignment_Bosch.Integration.Configurations
{
	public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> entity)
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__AuditLogs");
            entity.Property(e => e.Payload);
            entity.Property(e => e.Type);
            entity.Property(e => e.CorrelationId);
        }
    
    }
}

