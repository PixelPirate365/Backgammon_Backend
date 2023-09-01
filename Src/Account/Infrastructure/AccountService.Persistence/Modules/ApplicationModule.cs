using AccountService.Application.Common.Interfaces.Repository;
using AccountService.Application.Interfaces.Transaction;
using AccountService.Domain.Entities;
using AccountService.Persistence.Data;
using AccountService.Persistence.Repositories;
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
            services.AddScoped<ITransactionService>(provider => provider.GetService<ApplicationDbContext>()!);
            services.ResolveRepositories();
            return services;
        }
        private static void ResolveRepositories(this IServiceCollection services) {
            services.AddScoped<IRepository<AccountProfile>, Repository<AccountProfile>>();
            services.AddScoped<IRepository<FriendRequest>, Repository<FriendRequest>>();

        }
    }
}
