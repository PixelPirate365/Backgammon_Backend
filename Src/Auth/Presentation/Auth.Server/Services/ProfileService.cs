using Auth.Server.Entities;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Security.Claims;

namespace Auth.Server.Services {
    public class ProfileService : IProfileService {
        private readonly UserManager<User> _userManager;

        public ProfileService(UserManager<User> userManager) {
            _userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            var roles = await _userManager.GetRolesAsync(user);
            if (user == null) {
                return;
            };
            var claims = new List<Claim>
        {

            new Claim(JwtClaimTypes.GivenName, user.FirstName),
            new Claim(JwtClaimTypes.FamilyName, user.LastName),
            new Claim(JwtClaimTypes.Role,roles.FirstOrDefault()),
            new Claim(JwtClaimTypes.Email, user.Email),

        };
            context.IssuedClaims.AddRange(claims);

        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
