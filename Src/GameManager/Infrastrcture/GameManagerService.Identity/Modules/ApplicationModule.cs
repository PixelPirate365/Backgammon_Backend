using GameManagerService.Application.Interfaces;
using GameManagerService.Identity.Models;
using GameManagerService.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Identity.Modules {
    public static class ApplicationModule {
        public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services, IConfiguration configuration) {
            var jwtSettings = configuration.GetSection(nameof(JwtSettings));
            services.Configure<JwtSettings>(jwtSettings);
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<ITokenService, TokenService>();
            return services;
        }
    }
}
