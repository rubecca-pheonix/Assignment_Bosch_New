using System;
namespace Assignment_Bosch.Integration
{
	public class AuditLog
	{   
        public int Id { get; set; }
        public required string CorrelationId { get; set; }
        public required string Payload { get; set; }
        public required string Type { get; set; }
    }
}

