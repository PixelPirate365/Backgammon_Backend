using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.MessageBus.Interfaces {
    public interface IRabbitMQMessageSender {
        bool SendMessage(object message,string queueName);
    }
}
