using System;
namespace Assignment_Bosch.Services
{
	public interface IMessageProducer
	{
        void SendMessage<T>(T message);

    }
}

