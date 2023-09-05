using AuthApi.Common.Options.RabbitMQ;
using AuthService.MessageBus.Interfaces;
using AuthService.MessageBus.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.MessageBus.Modules {
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
