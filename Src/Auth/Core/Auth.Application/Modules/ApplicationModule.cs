using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Auth.Application.Modules {
    public static class ApplicationModule {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services) {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }

    }
}
