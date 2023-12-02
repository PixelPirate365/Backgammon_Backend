using AccountService.Common.Options.RabbitMQ;
using AccountService.MessageBus.Interfaces;
using AccountService.MessageBus.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.MessageBus.Modules {
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
