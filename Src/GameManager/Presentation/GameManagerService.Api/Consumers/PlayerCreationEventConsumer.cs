using GameManagerService.Application.Handlers.Profile.Commands;
using GameManagerService.Common.Constants;
using GameManagerService.Common.EventModels;
using GameManagerService.Common.Options.RabbitMQ;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace GameManagerApi.Consumers {
    public class PlayerCreationEventConsumer : BackgroundService {
        public string QueueName => EventNameConstants.PlayerCreationEvent;
        readonly ILogger<PlayerCreationEventConsumer> _logger;
        readonly IModel _channel;
        readonly IMediator _mediator;
        readonly IConnection _connection;

        public PlayerCreationEventConsumer(
            ILogger<PlayerCreationEventConsumer> logger,
            IModel channel,
            IMediator mediator,
            RabbitMQOptions rabbitMQOptions)
        {
            _logger = logger;
            _channel = channel;
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
                var player = JsonConvert.DeserializeObject<PlayerInfoModel>(content);
                HandleMessage(player).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(QueueName, false, consumer);
        }
        private async Task HandleMessage(PlayerInfoModel player) {
            var command = new CreatePlayerCommand() {
                AccountId = player.AccountId,
                PlayerColor = player.PlayerColor,
                UserId = player.UserId
            };
            await _mediator.Send(command);
        }
    }
}
