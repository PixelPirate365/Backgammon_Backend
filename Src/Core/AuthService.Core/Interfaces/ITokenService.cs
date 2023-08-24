using AuthService.Common.Responses;
using AuthService.Domain.Entities;

namespace AuthService.Application.Interfaces {
    public interface ITokenService {
        Task<Response<AuthenticationResponse>> GenerateUserTokenAsync(ApplicationUser applicationUser);
    }
}
