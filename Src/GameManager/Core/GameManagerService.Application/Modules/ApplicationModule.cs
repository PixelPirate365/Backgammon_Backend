﻿using AutoMapper;
using FluentValidation;
using GameManagerService.Application.Behaviours;
using GameManagerService.Application.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GameManagerService.Application.Modules {
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
            var playerMappingProfile = MapperConfigurationProfile.PlayerMappingProfile();
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(playerMappingProfile);
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
