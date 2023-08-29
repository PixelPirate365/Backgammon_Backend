using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace AuthService.Application.Handlers.User.Commands.RefreshUserToken {
    public class RefreshUserTokenCommandHandler : IRequestHandler<RefreshUserTokenCommand, Response<AuthenticationResponse>> {
        readonly ILogger<RefreshUserTokenCommandHandler> _logger;
        readonly ITokenService _tokenService;
        readonly IRepository<RefreshToken> _repository;
        readonly IIdentityService _identityService;
        public RefreshUserTokenCommandHandler(ILogger<RefreshUserTokenCommandHandler> logger,
            ITokenService tokenService,
            IRepository<RefreshToken> repository,
            IIdentityService identityService) {
            _logger = logger;
            _tokenService = tokenService;
            _repository = repository;
            _identityService = identityService;
        }
        public async Task<Response<AuthenticationResponse>> Handle(RefreshUserTokenCommand request,
            CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(RefreshUserTokenCommandHandler)}");
            var validatedToken = _tokenService.GetClaimsPrincipalFromToken(request.Token);
            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTimeUtc =
                new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(expiryDateUnix);
            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            var storedRefreshToken = _repository.TableNoTracking.SingleOrDefault(x => x.Token == new Guid(request.RefreshToken));
            var validation = IsTokenValid(expiryDateTimeUtc, storedRefreshToken, jti);
            if (!validation.Successful) {
                _logger.LogError($"{validation.Message}");
                return validation;
            }
            storedRefreshToken.Used = true;
            await _repository.Update(storedRefreshToken);
            var userId = validatedToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _identityService.FindByIdAsync(userId);
            var userToken = await _tokenService.GenerateUserTokenAsync(user);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(RefreshUserTokenCommandHandler)}");
            return userToken;
        }
        private static Response<AuthenticationResponse> IsTokenValid(DateTime expiryDateTimeUtc, RefreshToken storedRefreshToken, string jti) {
            if (expiryDateTimeUtc > DateTime.UtcNow)
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.TokenNotExpired
                };
            if (storedRefreshToken == null)
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.TokenNotExist
                };
            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.RefreshTokenExpired
                };
            if (storedRefreshToken.Invalidated)
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.InvalidateRefreshToken
                };
            if (storedRefreshToken.Used)
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.UsedRefreshToken
                };
            if (storedRefreshToken.JwtId != jti)
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.RefreshTokenNotMatchJWT
                };
            return new Response<AuthenticationResponse> {
                Successful = true
            };
        }
    }
}
