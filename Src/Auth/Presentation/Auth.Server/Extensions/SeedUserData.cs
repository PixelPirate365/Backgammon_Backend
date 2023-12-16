using Auth.Server.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Auth.Server.Extensions {
    public class SeedUserData {
        public static void InsertSeedData(string connectionString) {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<UserContext>(options => {
                options.UseSqlServer(connectionString);
            });
            services.AddIdentity<User, IdentityRole>(o => {
                o.Password.RequireDigit = false;
                o.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<UserContext>().AddDefaultTokenProviders();
            using (var serviceProvider = services.BuildServiceProvider()) {
            
            }
        }

    }
}
