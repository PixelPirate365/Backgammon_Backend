using AccountService.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Persistence.Modules {
    public static class ApplicationModule {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    });
            });
            return services;
        }
    }
}
