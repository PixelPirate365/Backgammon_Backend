using Auth.Common.Options.RabbitMQ;
using Auth.MessageBus.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.MessageBus.Services {
    public class RabbitMQMessageSender : IRabbitMQMessageSender {
        readonly RabbitMQOptions _busOptions;
        readonly ILogger<RabbitMQMessageSender> _logger;
        IConnection _connection;
        public RabbitMQMessageSender(RabbitMQOptions busOptions,
            ILogger<RabbitMQMessageSender> logger) {
            _busOptions = busOptions;
            _logger = logger;
        }
        public bool SendMessage(object message, string queueName) {
            if (ConnectionExists()) {
                using var channel = _connection.CreateModel();
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                return true;
            }
            return false;
        }
        private void CreateConnection() {
            try {
                var factory = new ConnectionFactory {
                    HostName = _busOptions.HostName,
                    UserName = _busOptions.Username,
                    Password = _busOptions.Password
                };
                _connection = factory.CreateConnection();
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Something went wrong in RabbitMQ sender");
            }
        }
        private bool ConnectionExists() {
            if (_connection != null) {
                return true;
            }
            CreateConnection();
            return _connection != null;
        }
    }
}
