using Auth.Server.Entities;
using Auth.Server.Interfaces;
using Auth.Server.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Auth.Server.Extensions {
    public class SeedUserData {
        public static void InsertSeedData(string connectionString) {
            var services = new ServiceCollection();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
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
                    CreateRoles(scope);
                    CreateUser(scope, "hermoni", "or", Guid.NewGuid(), "hermoniPass", "orher365@gmail.com", "Admin");
                    CreateUser(scope, "hermoni2", "or", Guid.NewGuid(), "hermoniPass2", "orwwwe@gmail.com", "User");
                }
            }
        }
        private static void CreateRoles(IServiceScope scope) {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var existingRoles = roleManager.Roles.ToList();
            var newRoles = new List<IdentityRole>()
            {
                new IdentityRole(){Name="Admin"},
                new IdentityRole(){Name="User"}
            };
            var result = newRoles.Where(er => existingRoles.All(newR => !string.Equals(newR.Name, er.Name, StringComparison.CurrentCultureIgnoreCase)));
            foreach (var role in result) {
                roleManager.CreateAsync(role).GetAwaiter().GetResult();
            }
        }
        private static void CreateUser(IServiceScope scope, string firstName, string lastName, Guid id, string password, string email, string role) {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var user = userManager.FindByEmailAsync(email).Result;
            if (user is null) {
                user = new User() {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    EmailConfirmed = true,
                    Id = id.ToString()
                };
                var result = userManager.CreateAsync(user, password).Result;
                CheckResult(result);
                result = userManager.AddToRoleAsync(user, role).Result;
                CheckResult(result);
                result = userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(JwtClaimTypes.Id, user.Id),
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.GivenName,user.FirstName),
                    new Claim(JwtClaimTypes.Role,role),
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
