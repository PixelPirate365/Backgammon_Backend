using AuthService.Application.Behaviours;
using AuthService.Application.Mappings;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AuthService.Application.Modules {
    public static class ApplicationModule {
        public static IServiceCollection ConfigureApplication(this IServiceCollection services) {

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
            services.AddApplication();
            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            var userMappingProfile = MapperConfigurationProfile.UserMappingProfile();
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(userMappingProfile);
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
