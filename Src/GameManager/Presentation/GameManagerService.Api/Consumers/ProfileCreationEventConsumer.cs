using GameManagerService.Common.Constants;
using GameManagerService.Common.Options.RabbitMQ;
using MediatR;
using RabbitMQ.Client;

namespace GameManagerApi.Consumers {
    public class ProfileCreationEventConsumer : BackgroundService {
        public string QueueName => EventNameConstants.PlayerCreationEvent;
        readonly ILogger<ProfileCreationEventConsumer> _logger;
        readonly IModel _channel;
        readonly IMediator _mediator;
        readonly IConnection _connection;

        public ProfileCreationEventConsumer(
            ILogger<ProfileCreationEventConsumer> logger,
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
        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            throw new NotImplementedException();
        }
    }
}
