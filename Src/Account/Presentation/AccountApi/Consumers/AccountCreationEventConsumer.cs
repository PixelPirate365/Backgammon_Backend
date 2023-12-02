﻿using AccountApi.Interfaces;
using AccountService.Application.Handlers.Account.Commands.CreateAccountProfile;
using AccountService.Common.Constants;
using AccountService.Common.EventModels;
using AccountService.Common.Options.RabbitMQ;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace AccountApi.Consumers {
    public class AccountCreationEventConsumer : BackgroundService {
        public string QueueName => EventNameConstants.AccountProfileCreationEvent;
        readonly ILogger<AccountCreationEventConsumer> _logger; 
        IConnection _connection;
        readonly IModel _channel;
        readonly IMediator _mediator;
        public AccountCreationEventConsumer(
            ILogger<AccountCreationEventConsumer> logger,
            RabbitMQOptions rabbitMQOptions,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            var factory = new ConnectionFactory {
                HostName = rabbitMQOptions.HostName,
                UserName = rabbitMQOptions.Username,
                Password = rabbitMQOptions.Password
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: QueueName, false, false, false, arguments: null);
        }
        //erlang 26.0.2
        //3.12.4 rabbit
        protected async override Task ExecuteAsync(CancellationToken stoppingToken) {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) => {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                UserAccountEventModel checkoutHeaderDto = JsonConvert.DeserializeObject<UserAccountEventModel>(content);
                HandleMessage(checkoutHeaderDto).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(QueueName, false, consumer);  
        }
        private async Task HandleMessage(UserAccountEventModel account) {
            CreateAccountProfileCommand command = new() {
                UserId = account.Id,
                Nickname = account.Username
            };
            await _mediator.Send(command);
            
        }
    }
}
