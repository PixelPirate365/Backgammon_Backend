using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AuthService.Application.Handlers.User.Commands.ChangeUserPassword {
    public class ChangeUserPasswordCommandHandler : IRequestHandler<ChangeUserPasswordCommand, Response> {
        private readonly ILogger<ChangeUserPasswordCommandHandler> _logger;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        public ChangeUserPasswordCommandHandler(IIdentityService identityService,
            ILogger<ChangeUserPasswordCommandHandler> logger,
            ICurrentUserService currentUserService) {
            _identityService = identityService; 
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public async Task<Response> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(ChangeUserPasswordCommandHandler)}");
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
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(ChangeUserPasswordCommandHandler)}");
            return new Response {
                Successful = true,
                Message = ResponseMessageConstants.PasswordChangedSuccessfully
            };
        }
    }
}
