using GameManagerService.Application.Handlers.Profile.Commands.CreatePlayer;
using GameManagerService.Application.Interfaces.Repository;
using GameManagerService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GameManagerService.Application.Handlers.Profile.Commands.DeletePlayer {
    public class DeletePlayerCommandHandler : IRequestHandler<DeletePlayerCommand, Unit> {
        readonly ILogger<CreatePlayerCommandHandler> _logger;
        readonly IRepository<Player> _playerRepository;
        public DeletePlayerCommandHandler(
            ILogger<CreatePlayerCommandHandler> logger,
            IRepository<Player> playerRepository)
        {
            _logger = logger;
            _playerRepository = playerRepository;
        }
        public async Task<Unit> Handle(DeletePlayerCommand request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(DeletePlayerCommandHandler)}");
            var user = await _playerRepository.TableNoTracking.FirstOrDefaultAsync( x => x.UserId == request.UserId );
            await _playerRepository.Delete(user );
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(DeletePlayerCommandHandler)}");
            return Unit.Value;
        }
    }
}
