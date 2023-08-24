using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Identity.Services {
    public class IdentityService : IIdentityService {

        private readonly UserManager<ApplicationUser> _userManager;
        public IdentityService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword) => await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password) => await _userManager.CheckPasswordAsync(user, password);

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password) => await _userManager.CreateAsync(applicationUser, password);

        public async Task<ApplicationUser> FindByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

        public async Task<ApplicationUser> FindByIdAsync(string id) => await _userManager.FindByIdAsync(id);
    }
}
