using System;
namespace Assignment_Bosch.Models
{
	public class ApplicationConfigurations
	{
		public required RabbitMqProperties RabbitMqProperties { get; set; }
		public required Publisher Publisher { get; set; }
	}

	public class RabbitMqProperties
	{
		public required string Host { get; set; }
		public int Port { get; set; }
		public required string Username { get; set; }
		public required string Password { get; set; }
	}

	public class Publisher
	{
		public required string Exchange { get; set; }
        public required string Type { get; set; }
        public required string RoutingKey { get; set; }
        public bool Persistent { get; set; }
        public bool Durable { get; set; }
        public bool AutoDelete { get; set; }

    }
}

