using AuthApi.Common.Constants;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using AuthService.MessageBus.Interfaces;
using AuthService.MessageBus.Services;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Handlers.User.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<AuthenticationResponse>> {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IRabbitMQMessageSender _rabbitMQMessageSender;
        public CreateUserCommandHandler(IIdentityService identityService,
            ITokenService tokenService,
            ILogger<CreateUserCommandHandler> logger,
            IMapper mapper,
            IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _identityService = identityService;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }
        public async Task<Response<AuthenticationResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(CreateUserCommandHandler)}");

            var user = _mapper.Map<ApplicationUser>(request); // CREATE DTO FOR EVENT EMISSION

            var result = await _identityService.CreateUserAsync(user, request.Password);
            if (!result.Succeeded) {
                _logger.LogError(ResponseMessageConstants.UserCreationFailed);
                return new Response<AuthenticationResponse> {
                    Successful = false,
                    Message = ResponseMessageConstants.UserCreationFailed,
                };
            }
            var response = await _tokenService.GenerateUserTokenAsync(user);
            _rabbitMQMessageSender.SendMessage(user,EventNameConstants.AccountProfileCreationEvent);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(CreateUserCommandHandler)}");
            return response;
        }
    }
}
