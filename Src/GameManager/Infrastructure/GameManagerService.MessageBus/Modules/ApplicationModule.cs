using GameManagerService.Common.Options.RabbitMQ;
using GameManagerService.MessageBus.Interfaces;
using GameManagerService.MessageBus.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.MessageBus.Modules {
    public static class ApplicationModule {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration) {
            var rabbitMQOptions = new RabbitMQOptions();
            configuration.GetSection(nameof(RabbitMQOptions)).Bind(rabbitMQOptions);
            services.AddSingleton(rabbitMQOptions);
            services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
            return services;
        }
    }
}
