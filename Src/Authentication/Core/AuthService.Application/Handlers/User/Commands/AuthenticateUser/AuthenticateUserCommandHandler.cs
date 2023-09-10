using AuthApi.Common.Enums;
using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Handlers.User.Commands.AuthenticateUser {
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Response<AuthenticationResponse>> {
        private readonly ILogger<AuthenticateUserCommandHandler> _logger;
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly IRepository<ApplicationUser> _applicationUserRepository;
        public AuthenticateUserCommandHandler(
            ILogger<AuthenticateUserCommandHandler> logger,
            IIdentityService identityService,
            ITokenService tokenService,
            IRepository<ApplicationUser> applicationUserRepository) {
            _logger = logger;
            _identityService = identityService;
            _tokenService = tokenService;
            _applicationUserRepository = applicationUserRepository;
        }
        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(AuthenticateUserCommandHandler)}");
            var user = await _identityService.FindByEmailAsync(request.Email);
            if (user == null) {
                _logger.LogError(ResponseMessageConstants.InvalidEmailPassword);
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.InvalidEmailPassword
                };
            };
            if (user.IsDeleted) {
                _logger.LogError(ResponseMessageConstants.UserDeactivated);
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.UserDeactivated
                };
            }
            var passwordCheck = await _identityService.CheckPasswordAsync(user, request.Password);
            if (!passwordCheck)
                return new Response<AuthenticationResponse> { Successful = false, Message = ResponseMessageConstants.InvalidEmailPassword };
            var response = await _tokenService.GenerateUserTokenAsync(user);
            user.Status = (int)UserStatusEnum.LoggedIn;
            await _applicationUserRepository.Update(user);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(AuthenticateUserCommandHandler)}");
            return response;
        }
    }
}
