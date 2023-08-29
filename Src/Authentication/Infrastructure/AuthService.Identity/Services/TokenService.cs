using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using AuthService.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Identity.Services {
    public class TokenService : ITokenService {
        private readonly JwtSettings _jwtSettings;
        private readonly IRepository<RefreshToken> _repository;
        public TokenService(IOptions<JwtSettings> jwtSettings,
            IRepository<RefreshToken> repository) {
            _jwtSettings = jwtSettings.Value;
            _repository = repository;
        }
        public async Task<Response<AuthenticationResponse>> GenerateUserTokenAsync(ApplicationUser applicationUser) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, applicationUser.Id),
                    new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken {
                JwtId = token.Id,
                UserId = applicationUser.Id,
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };
            await _repository.Add(refreshToken);
            return new Response<AuthenticationResponse>() {
                Successful = true,
                Message = ResponseMessageConstants.SuccessfulyLogin,
                Result = new AuthenticationResponse() {
                    Email = applicationUser.Email,
                    Token = tokenHandler.WriteToken(token),
                    RefreshToken = refreshToken.Token.ToString(),
                    Username = applicationUser.UserName
                }
            };
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
