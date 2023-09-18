using System.Text;
using Assignment_Bosch.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Assignment_Bosch.Services
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly RabbitMqConnectionManager _rabbitMqConnectionManager;
        private readonly ApplicationConfigurations _configurations;
        private readonly ILogger<RabbitMQProducer> _logger;

        public RabbitMQProducer(RabbitMqConnectionManager rabbitMqConnectionManager,
            IOptions<ApplicationConfigurations> options, ILogger<RabbitMQProducer> logger)
        {
            _rabbitMqConnectionManager = rabbitMqConnectionManager;
            _configurations = options.Value;
            _logger = logger;
        }
        public void SendMessage<T>(T message)
        {
            try
            {
                var channel = _rabbitMqConnectionManager.Connection?.CreateModel();

                if (message == null)
                return;

            
                if(channel != null)
                {


                    channel.ExchangeDeclare(_configurations.Publisher.Exchange, _configurations.Publisher.Type, _configurations.Publisher.Durable, _configurations.Publisher.AutoDelete, null);

                    var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = _configurations.Publisher.Persistent;

                    channel.BasicPublish(_configurations.Publisher.Exchange, _configurations.Publisher.RoutingKey, properties, sendBytes);
                    channel.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}

