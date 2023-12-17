using Auth.Server.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                    CreateUser(scope, "hermoni", "or", Guid.NewGuid(), "hermoniPass", "orher365@gmail.com");
                    CreateUser(scope, "hermoni2", "or", Guid.NewGuid(), "hermoniPass2", "orwwwe@gmail.com");

                }
            }
        }
        private static void CreateUser(IServiceScope scope, string firstName, string lastName, Guid id, string password, string email) {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = userManager.FindByEmailAsync(email).Result;
            if (user is null) {
                user = new User() {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,

                    Id = id.ToString()
                };
                var result = userManager.CreateAsync(user, password).Result;
                CheckResult(result);
                result = userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(JwtClaimTypes.GivenName,user.FirstName),
                    new Claim(JwtClaimTypes.FamilyName,user.LastName),

                }).Result;
                CheckResult(result);
            }

        }
        private static void CheckResult(IdentityResult result) {
            if (!result.Succeeded) {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
