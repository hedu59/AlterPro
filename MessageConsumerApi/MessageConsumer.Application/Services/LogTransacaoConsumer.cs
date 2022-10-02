using MessageConsumer.Application.Models;
using MessageConsumer.Domain.Models;
using MessageConsumer.Infra;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageConsumer.Application.Services
{
    public class LogTransacaoConsumer : BackgroundService
    {
        private readonly ITransacaoMongoRepository _mongoRepository;
        private readonly RabbitMqConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public LogTransacaoConsumer(IOptions<RabbitMqConfiguration> option, ITransacaoMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
            _configuration = option.Value;
            var factory = new ConnectionFactory
            {
                HostName = _configuration.Host
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: _configuration.Queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }


        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, eventArgs) =>
            {
                var contentArray = eventArgs.Body.ToArray();
                var contentString = Encoding.UTF8.GetString(contentArray);
                var message = JsonConvert.DeserializeObject<LogTransacaoMessage>(contentString);

                CriarLogTransacao(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.Queue, false, consumer);
            return Task.CompletedTask;
        }

        public void CriarLogTransacao(LogTransacaoMessage log)
        {
            var logTransacao = new LogTransacao()
            {
                Id = ObjectId.Parse(log.Id),
                Observacao = log.Observacao,
                RegisterDate = log.RegisterDate, 
                ServidorId = log.ServidorId,
            };
            _mongoRepository.CreateAsync(logTransacao);
        }
    }
}

