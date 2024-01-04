using AccountService.Application.Handlers.Account.Commands.CreateAccountProfile;
using AccountService.Common.Constants;
using AccountService.Common.EventModels;
using MediatR;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using AccountService.Common.Options.RabbitMQ;
using System.Text;
using Newtonsoft.Json;
using AccountService.Application.Handlers.Account.Commands.DeleteAccount;

namespace AccountApi.Consumers {
    public class UserProfileDeletionEventConsumer: BackgroundService {
        public string QueueName => EventNameConstants.UserProfileDeletionEvent;
        readonly ILogger<UserProfileDeletionEventConsumer> _logger;
        IConnection _connection;
        readonly IModel _channel;
        readonly IMediator _mediator;
        public UserProfileDeletionEventConsumer(
            ILogger<UserProfileDeletionEventConsumer> logger,
            RabbitMQOptions rabbitMQOptions,
            IMediator mediator) {
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
                Guid userId = JsonConvert.DeserializeObject<Guid>(content);
                HandleMessage(userId).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(QueueName, false, consumer);
        }
        private async Task HandleMessage(Guid userId) {
            var command = new DeleteAccountProfileCommand() {
                UserId = userId
            };
            await _mediator.Send(command);
        }
    }
}
