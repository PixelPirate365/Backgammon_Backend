using AccountService.Application.Behaviours;
using AccountService.Application.Mappings;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AccountService.Application.Modules {
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
        private static IServiceCollection AddApplication(this IServiceCollection services) {
            var accountMappingProfile = MapperConfigurationProfile.AccountMappingProfile();
            var friendRequestMappingProfile = MapperConfigurationProfile.FriendRequestMappingProfile();
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(accountMappingProfile);
                mc.AddProfile(friendRequestMappingProfile);
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
