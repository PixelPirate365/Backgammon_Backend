using Auth.Application.Handlers.User.Commands.CreateUserProfile;
using Auth.Common.Constants;
using Auth.MessageBus.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Handlers.User.Commands.DeleteUserProfile {
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, Unit> {
        readonly ILogger<DeleteUserProfileCommandHandler> _logger;
        readonly IRabbitMQMessageSender _messageSender;
        public DeleteUserProfileCommandHandler(
            ILogger<DeleteUserProfileCommandHandler> logger,
            IRabbitMQMessageSender messageSender)
        {
            _logger = logger;
            _messageSender = messageSender;
        }
        public async Task<Unit> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(DeleteUserProfileCommandHandler)}");
            
            _messageSender.SendMessage(request.UserId, EventNameConstants.UserProfileDeletionEvent);

            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(DeleteUserProfileCommandHandler)}");
            await Task.Delay(0);
            return Unit.Value;
        }
    }
}
