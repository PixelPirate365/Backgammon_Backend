using AutoMapper;
using GameManagerService.Application.Interfaces.Repository;
using GameManagerService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Handlers.Profile.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, Unit>
    {

        readonly ILogger<CreatePlayerCommandHandler> _logger;
        readonly IRepository<Player> _playerRepository;
        readonly IMapper _mapper;

        public CreatePlayerCommandHandler(
            ILogger<CreatePlayerCommandHandler> logger,
            IMapper mapper,
            IRepository<Player> playerRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _playerRepository = playerRepository;
        }
        public async Task<Unit> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(CreatePlayerCommandHandler)}");
            var player = _mapper.Map<Player>(request);
            await _playerRepository.Add(player);
            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(CreatePlayerCommandHandler)}");
            return Unit.Value;
        }
    }
}
