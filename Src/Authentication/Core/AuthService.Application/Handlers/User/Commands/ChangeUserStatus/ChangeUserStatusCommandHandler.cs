using AuthApi.Common.Enums;
using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AuthService.Application.Handlers.User.Commands.ChangeUserStatus {
    public class ChangeUserStatusCommandHandler : IRequestHandler<ChangeUserStatusCommand, UserStatusEnum> {
        readonly ILogger<ChangeUserPasswordCommandHandler> _logger;
        readonly IRepository<ApplicationUser> _repository;
        readonly ICurrentUserService _currentUserService;

        public ChangeUserStatusCommandHandler(
            ILogger<ChangeUserPasswordCommandHandler> logger,
            IRepository<ApplicationUser> repository,
            ICurrentUserService currentUserService) {
            _logger = logger;
            _repository = repository;
            _currentUserService = currentUserService;
        }
        public async Task<UserStatusEnum> Handle(ChangeUserStatusCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(ChangeUserStatusCommandHandler)}");
            var user = await _repository.TableNoTracking.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId);
            user.Status = (int)request.UserStatus;
            await _repository.Update(user);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(ChangeUserStatusCommandHandler)}");
            return (UserStatusEnum)user.Status;
        }
    }
}
