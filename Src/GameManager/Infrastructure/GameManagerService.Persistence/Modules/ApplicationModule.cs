using GameManagerService.Application.Interfaces.Repository;
using GameManagerService.Domain.Entities;
using GameManagerService.Persistence.Data;
using GameManagerService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Persistence.Modules {
    public static class ApplicationModule {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration) {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    });
            });
            services.ResolveRepositories();
            return services;
        }
        private static void ResolveRepositories(this IServiceCollection services) {
            services.AddScoped<IRepository<Player>, Repository<Player>>();
            services.AddScoped<IRepository<Game>, Repository<Game>>();
            services.AddScoped<IRepository<GamePlayers>, Repository<GamePlayers>>();
            services.AddScoped<IRepository<Move>, Repository<Move>>();
            services.AddScoped<IRepository<GameState>, Repository<GameState>>();
            services.AddScoped<IRepository<MatchMaking>, Repository<MatchMaking>>();
            services.AddScoped<IRepository<FriendGameRequest>, Repository<FriendGameRequest>>();
        }
    }
}
