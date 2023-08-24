using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Handlers.User.Queries.AuthenticateUser {
    public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery, Response<AuthenticationResponse>> {
        private readonly ILogger<AuthenticateUserQueryHandler> _logger;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITokenService _tokenService;
        public AuthenticateUserQueryHandler(ILogger<AuthenticateUserQueryHandler> logger,
            IIdentityService identityService,
            ICurrentUserService currentUserService,
            ITokenService tokenService) {
            _logger = logger;
            _identityService = identityService;
            _currentUserService = currentUserService;
            _tokenService = tokenService;
        }
        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(AuthenticateUserQueryHandler)}");
            var user = await _identityService.FindByEmailAsync(request.Email);
            if (user == null) {
                _logger.LogError(ResponseMessageConstants.InvalidEmailPassword);
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.InvalidEmailPassword
                };
            };
            var passwordCheck = await _identityService.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
                return new Response<AuthenticationResponse> { Successful = false, Message = ResponseMessageConstants.InvalidEmailPassword };
            var response = await _tokenService.GenerateUserTokenAsync(user);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(AuthenticateUserQueryHandler)}");
            return response;
        }
    }
}
