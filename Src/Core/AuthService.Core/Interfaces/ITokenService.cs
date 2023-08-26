using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using System.Security.Claims;

namespace AuthService.Application.Interfaces {
    public interface ITokenService {
        Task<Response<AuthenticationResponse>> GenerateUserTokenAsync(ApplicationUser applicationUser);
        ClaimsPrincipal GetClaimsPrincipalFromToken(string token);
    }
}
