using System;
using Assignment_Bosch.Models;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Assignment_Bosch.Services
{
	public class RabbitMqConnectionManager
	{
		private readonly ApplicationConfigurations _configurations;
        public IConnection? Connection { get; private set; }
        private ILogger<RabbitMqConnectionManager> _logger;

        public RabbitMqConnectionManager(IOptions<ApplicationConfigurations> options, ILogger<RabbitMqConnectionManager> logger)
		{
			_configurations = options.Value;
            _logger = logger;
            CreateConnection();
		}

		private void CreateConnection()
		{
			try
			{
                var factory = new ConnectionFactory
                {
                    HostName = _configurations.RabbitMqProperties.Host,
                    Port = _configurations.RabbitMqProperties.Port,
                    Ssl = new SslOption
                    {
                        Enabled = true,
                        ServerName = _configurations.RabbitMqProperties.Host
                    }
                };
                factory.UserName = _configurations.RabbitMqProperties.Username;
                factory.Password = _configurations.RabbitMqProperties.Password;
                //factory.AmqpUriSslProtocols
                Connection = factory.CreateConnection();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
	}
}

