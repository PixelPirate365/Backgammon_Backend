using IdentityServer4.Models;

namespace Auth.Server.Services {
    public interface IProfileService {
        Task GetProfileDataAsync(ProfileDataRequestContext context);
        Task IsActiveAsync(IsActiveContext context);
    }
}
