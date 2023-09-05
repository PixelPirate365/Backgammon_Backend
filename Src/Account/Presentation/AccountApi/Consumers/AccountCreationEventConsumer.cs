using AccountApi.Interfaces;
using AccountService.Common.Constants;
using AccountService.Common.EventModels;
using AccountService.Common.Options.RabbitMQ;
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
        public AccountCreationEventConsumer(ILogger<AccountCreationEventConsumer> logger,
            RabbitMQOptions rabbitMQOptions)
        {
            _logger = logger;
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
            stoppingToken.ThrowIfCancellationRequested();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) => {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());
                UserAccountEventModel checkoutHeaderDto = JsonConvert.DeserializeObject<UserAccountEventModel>(content);
                HandleMessage(checkoutHeaderDto);
                _channel.BasicAck(ea.DeliveryTag, false);
            };
            _channel.BasicConsume(QueueName, false, consumer);
            return Task.CompletedTask;
        }
        private void HandleMessage(UserAccountEventModel account) {
            Console.WriteLine(account.Id);
            
        }
    }
}
