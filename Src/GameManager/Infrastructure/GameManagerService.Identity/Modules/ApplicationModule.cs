using GameManagerService.Application.Interfaces;
using GameManagerService.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GameManagerService.Identity.Modules
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddIdentityAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            return services;
        }
    }
}
