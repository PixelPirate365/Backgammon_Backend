using GameManagerService.Application.Handlers.Players.Queries.GetOpponentPlayerBalance;
using GameManagerService.Common.Constants;
using GameManagerService.Common.EventModels;
using GameManagerService.Common.Options.RabbitMQ;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace GameManagerApi.Consumers {
    public class PlayerBalanceConsumer : BackgroundService {
        public string QueueName => EventNameConstants.CheckRecieverBalanceEvent;
        readonly ILogger<PlayerDeletionEventConsumer> _logger;
        readonly IModel _channel;
        readonly IMediator _mediator;
        readonly IConnection _connection;
        public PlayerBalanceConsumer(ILogger<PlayerDeletionEventConsumer> logger,
            IMediator mediator,
            RabbitMQOptions rabbitMQOptions) {
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
                var opponentPlayerBalance = JsonConvert.DeserializeObject<SendRecieverBalanceModel>(content);
                HandleMessage(opponentPlayerBalance).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(QueueName, false, consumer);
        }
        private async Task HandleMessage(SendRecieverBalanceModel opponentPlayerBalance) {
            var query = new GetOpponentPlayerQuery() {
                SenderId = opponentPlayerBalance.SenderId,
                BalanceAmount = opponentPlayerBalance.BalanceAmount,
                RecieverId = opponentPlayerBalance.RecieverId,
                BetAmount = opponentPlayerBalance.BetAmount
            };
            await _mediator.Send(query);
        }
    }
}
