using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Application.Interfaces;
using AuthService.Common.Constants;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Handlers.User.Commands.DeleteUser {
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response> {
        readonly ICurrentUserService _currentUserService;
        readonly IIdentityService _identityService;
        readonly IRepository<ApplicationUser> _repository;
        readonly ILogger<DeleteUserCommandHandler> _logger;
        public DeleteUserCommandHandler(ICurrentUserService currentUserService,
            IIdentityService identityService,
            IRepository<ApplicationUser> repository,
            ILogger<DeleteUserCommandHandler> logger) {
            _currentUserService = currentUserService;
            _identityService = identityService;
            _repository = repository;
            _logger = logger;
        }
        public async Task<Response> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(DeleteUserCommandHandler)}");
            var userId = _currentUserService.UserId;
            var user = await _identityService.FindByIdAsync(userId);
            await _repository.Delete(user);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(DeleteUserCommandHandler)}");
            return new Response() {
                Successful = true,
                Message = ResponseMessageConstants.SuccefulyDeleted
            };
        }
    }
}
