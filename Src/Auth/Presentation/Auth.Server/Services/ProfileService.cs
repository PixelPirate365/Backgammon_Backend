using Auth.Server.Entities;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Auth.Server.Services {
    public class ProfileService : IProfileService {
        private readonly UserManager<User> _userManager;

        public ProfileService(UserManager<User> userManager) {
            _userManager = userManager;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context) {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null) {
                return;
            };
            var claims = new List<Claim>
        {

            new Claim(JwtClaimTypes.GivenName, user.FirstName),
            new Claim(JwtClaimTypes.FamilyName, user.LastName)

        };
            context.IssuedClaims.AddRange(claims);

        }

        public  Task IsActiveAsync(IsActiveContext context) {
            return Task.CompletedTask;
        }
    }
}
