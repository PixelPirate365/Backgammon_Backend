using Auth.Common.Constants;
using Auth.MessageBus.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Auth.Application.Handlers.User.CreateUserProfile {
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, Unit> {

        readonly ILogger<CreateUserProfileCommandHandler> _logger;
        readonly IRabbitMQMessageSender _rabbitMQMessageSender;
        public CreateUserProfileCommandHandler(
            ILogger<CreateUserProfileCommandHandler> logger,
            IRabbitMQMessageSender rabbitMQMessageSender
            ) {
            _logger = logger;
            _rabbitMQMessageSender = rabbitMQMessageSender;
        }
        public async Task<Unit> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(CreateUserProfileCommandHandler)}");
            _rabbitMQMessageSender.SendMessage(request, EventNameConstants.UserProfileCreationEvent);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(CreateUserProfileCommandHandler)}");
            return Unit.Value;
        }
    }
}
