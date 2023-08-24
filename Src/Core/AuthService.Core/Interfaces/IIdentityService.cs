using AuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Application.Interfaces {
    public interface IIdentityService {
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> FindByIdAsync(string id);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
        Task<IdentityResult> CreateUserAsync(ApplicationUser applicationUser, string password);
    }
}
