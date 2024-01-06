using GameManagerService.Application.Handlers.Profile.Commands.DeletePlayer;
using GameManagerService.Common.Constants;
using GameManagerService.Common.EventModels;
using GameManagerService.Common.Options.RabbitMQ;
using GameManagerService.Domain.Entities;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace GameManagerApi.Consumers {
    public class PlayerDeletionEventConsumer : BackgroundService {

        public string QueueName => EventNameConstants.PlayerDeletionEvent;
        readonly ILogger<PlayerDeletionEventConsumer> _logger;
        readonly IModel _channel;
        readonly IMediator _mediator;
        readonly IConnection _connection;
        public PlayerDeletionEventConsumer(ILogger<PlayerDeletionEventConsumer> logger,
            IMediator mediator,
            RabbitMQOptions rabbitMQOptions)
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
        protected async override Task ExecuteAsync(CancellationToken stoppingToken) {
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) => { 
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                var userId = JsonConvert.DeserializeObject<Guid>(content);
                HandleMessage(userId).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);

            };
            _channel.BasicConsume(QueueName, false, consumer);
        }

        async Task HandleMessage(Guid userId) {
            var command = new DeletePlayerCommand { UserId = userId };
            await _mediator.Send(command);
        }
    }
}
