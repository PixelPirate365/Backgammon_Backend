using AccountService.Application.Interfaces;
using AccountService.Identity.Models;
using AccountService.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Identity.Modules {
    public static class ApplicationModule {
        public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services, IConfiguration configuration) {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
