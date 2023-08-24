using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using AuthService.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Identity.Services {
    public class TokenService : ITokenService {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<ApplicationUser> _userManager;
        public TokenService(JwtSettings jwtSettings, UserManager<ApplicationUser> userManager) {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
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
            return new Response<AuthenticationResponse>() {
                Successful = true,
                Message = ResponseMessageConstants.SuccessfulyLogin,
                Result = new AuthenticationResponse() {
                    Email = applicationUser.Email,
                    Token = tokenHandler.WriteToken(token),
                    Username = applicationUser.UserName
                }
            };
        }
    }
}
