using Newtonsoft.Json;
using Prototype.Application.Filas.Models;
using Prototype.Application.Interfaces.Filas;
using RabbitMQ.Client;
using System.Text;

namespace Prototype.Application.Filas.Producers
{
    public class EmailProducer : IEmailProducer
    {
        private readonly ConnectionFactory _factory;
        private const string QUEUE_NAME = "mail_message";
        public EmailProducer()
        {
            _factory = new ConnectionFactory() { HostName = "localhost" };
        }

        public void ProduceEmail(InvitationMessage message)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: QUEUE_NAME, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var stringfieldMessage = JsonConvert.SerializeObject(message);
                    var bytesMessage = Encoding.UTF8.GetBytes(stringfieldMessage);

                    channel.BasicPublish(exchange: "", routingKey: QUEUE_NAME, basicProperties: null, body: bytesMessage);
                }
            }
        }
    }
}
