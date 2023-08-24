using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AuthService.Application.Handlers.User.Commands.ChangeUserPassword {
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response> {
        private readonly ILogger<ChangePasswordCommandHandler> _logger;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        public ChangePasswordCommandHandler(IIdentityService identityService,
            ILogger<ChangePasswordCommandHandler> logger,
            ICurrentUserService currentUserService) {
            _identityService = identityService; 
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(ChangePasswordCommandHandler)}");
            var user = await _identityService.FindByIdAsync(_currentUserService.UserId.ToString());
            if (user == null) {
                _logger.LogError(ResponseMessageConstants.UserNotFound);
                return new Response {
                    Successful = false,
                    Message = ResponseMessageConstants.UserNotFound
                };
            }
            var passwordCheck = await _identityService.CheckPasswordAsync(user, request.CurrentPassword);
            if (!passwordCheck) {
                _logger.LogError(ResponseMessageConstants.InvalidPassword);
                return new Response {
                    Successful = false,
                    Message = ResponseMessageConstants.InvalidPassword
                };
            }
            var result = await _identityService.ChangePasswordAsync(user,
                request.CurrentPassword,
                request.NewPassword);
            if (!result.Succeeded) {
                _logger.LogError(ResponseMessageConstants.UnableToChangePassword);
                return new Response {
                    Successful = false,
                    Message = ResponseMessageConstants.UnableToChangePassword
                };
            }
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(ChangePasswordCommandHandler)}");
            return new Response {
                Successful = true,
                Message = ResponseMessageConstants.PasswordChangedSuccessfully
            };
        }
    }
}
