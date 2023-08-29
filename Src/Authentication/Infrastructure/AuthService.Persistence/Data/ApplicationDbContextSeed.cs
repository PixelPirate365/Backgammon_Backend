using AuthService.Common.Constants;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Persistence.Data {
    public static class ApplicationDbContextSeed {
        public static async Task ApplicationDataSeed(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager) {
            await SeedUsersAsync(userManager);
        }
        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager) {
            var defaultUser = new ApplicationUser {
                UserName = SeedConstants.DefaultUsername,
                Email = SeedConstants.DefaultEmail
            };
            if (userManager.Users.All(u => u.UserName != defaultUser.UserName)) {
                await userManager.CreateAsync(defaultUser, SeedConstants.DefaultPassword);
            }
        }
    }
}
