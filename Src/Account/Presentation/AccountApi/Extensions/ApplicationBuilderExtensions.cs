using AccountApi.Consumers;
using AccountApi.Interfaces;

namespace AccountApi.Extensions {
    public static class ApplicationBuilderExtensions {
        public static IAccountCreationEventConsumer? AccountCreationEventConsumer { get; set; }
        public static IApplicationBuilder UseRabbitMQBusConsumer(this IApplicationBuilder app) {
            AccountCreationEventConsumer = app.ApplicationServices.GetService<IAccountCreationEventConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>()!;
            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopped.Register(OnStop);
            return app;
        }
        private static void OnStart() {
            AccountCreationEventConsumer?.Start();
        }
        private static void OnStop() {
            AccountCreationEventConsumer?.Stop();
        }
    }
}
