using AccountService.Application.Handlers.Account.Queries.GetAccountBalance;
using AccountService.Common.Constants;
using AccountService.Common.EventModels;
using AccountService.Common.Options.RabbitMQ;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace AccountApi.Consumers {
    public class CheckProfileBalanceConsumer : BackgroundService {
        public string QueueName => EventNameConstants.CheckRecieverBalanceEvent;
        readonly ILogger<CheckProfileBalanceConsumer> _logger;
        IConnection _connection;
        readonly IModel _channel;
        readonly IMediator _mediator;

        public CheckProfileBalanceConsumer(
            ILogger<CheckProfileBalanceConsumer> logger,
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
                var checkProfileBalance = JsonConvert.DeserializeObject<CheckRecieverBalanceModel>(content);
                HandleMessage(checkProfileBalance).GetAwaiter().GetResult();
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(QueueName, false, consumer);
        }
        private async Task HandleMessage(CheckRecieverBalanceModel model) {
            var query = new GetAccountBalanceQuery {
                BetAmount = model.BetAmount,
                RecieverId = model.RecieverId,
                SenderId = model.SenderId,
            };
            await _mediator.Send(query);
        }
    }
}
