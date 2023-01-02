using MessageConsumer.Application.Interfaces;
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
    public class InvitationConsumer : BackgroundService
    {
        private readonly ITransacaoMongoRepository _mongoRepository;
        private readonly RabbitMqConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public InvitationConsumer(IOptions<RabbitMqConfiguration> option, ITransacaoMongoRepository mongoRepository, IEmailService emailService)
        {
            _mongoRepository = mongoRepository;
            _emailService = emailService;
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
                var message = JsonConvert.DeserializeObject<InvitationMessage>(contentString);

                _emailService.SendEmailAsync(message);

                RegisterInvitation(message);
                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.Queue, false, consumer);
            return Task.CompletedTask;
        }

        public void RegisterInvitation(InvitationMessage message)
        {
            var invitation = new Invitation()
            {
                Id = ObjectId.Parse(message.Id),
                RegisterDate = message.RegisterDate, 
                Description = message.Description,
                InvitationId = message.InvitationId,
                InvitationPrice = message.InvitationPrice,
                ContactFullName = message.ContactFullName,
                ContactPhoneNumber = message.ContactPhoneNumber,  

            };
            _mongoRepository.CreateAsync(invitation);
        }
    }
}

