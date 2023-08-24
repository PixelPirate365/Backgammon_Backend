using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace AuthService.Application.Handlers.User.Commands.CreateUser {
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<AuthenticationResponse>> {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        public CreateUserCommandHandler(IIdentityService identityService,
            ITokenService tokenService,
            ILogger<CreateUserCommandHandler> logger,
            IMapper mapper)
        {
            _identityService = identityService;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<AuthenticationResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(CreateUserCommandHandler)}");
            var user = _mapper.Map<ApplicationUser>(request);

            var result = await _identityService.CreateUserAsync(user, request.Password);
            if (!result.Succeeded) {
                _logger.LogError(ResponseMessageConstants.UserCreationFailed);
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.UserCreationFailed,
                };
            }
            var response = await _tokenService.GenerateUserTokenAsync(user);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(CreateUserCommandHandler)}");
            return response;
        }
    }
}
