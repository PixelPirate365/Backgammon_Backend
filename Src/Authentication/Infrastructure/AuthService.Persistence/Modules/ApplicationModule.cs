using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Domain.Entities;
using AuthService.Persistence.Data;
using AuthService.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Persistence.Modules {
    public static class ApplicationModule {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    });
            });
            services.Configure<IdentityOptions>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
            });
            services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.ResolveRepositories();
            return services;
        }
        private static void ResolveRepositories(this IServiceCollection services) {
            services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
            services.AddScoped<IRepository<ApplicationUser>, Repository<ApplicationUser>>();

        }
    }

}
