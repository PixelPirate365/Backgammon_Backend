using AccountService.Application.Interfaces;
using AccountService.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AccountService.Identity.Services {
    public class TokenService : ITokenService {
        private readonly JwtSettings _jwtSettings;
        public TokenService(IOptions<JwtSettings> jwtSettings) {
            _jwtSettings = jwtSettings.Value;
        }
        public ClaimsPrincipal GetClaimsPrincipalFromToken(string token) {
            try {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
                var principle = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                return !IsJwtWithValidSecurityAlgorithm(validatedToken) ? null : principle;
            }
            catch (Exception e) {
                Console.WriteLine(e);
                throw;
            }

        }
        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) {
            return (validatedToken is JwtSecurityToken jwtSecurityToken)
                   && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
