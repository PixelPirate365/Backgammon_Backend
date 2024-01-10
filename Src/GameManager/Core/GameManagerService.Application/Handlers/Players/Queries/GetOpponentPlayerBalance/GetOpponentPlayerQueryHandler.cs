using GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest;
using GameManagerService.Application.Interfaces.Repository;
using GameManagerService.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Handlers.Players.Queries.GetOpponentPlayerBalance {
    public class GetOpponentPlayerQueryHandler : IRequestHandler<GetOpponentPlayerQuery,bool> {
        readonly ILogger<GetOpponentPlayerQueryHandler> _logger;
        readonly IRepository<FriendGameRequest> _friendGameRepository;
        public GetOpponentPlayerQueryHandler(
            ILogger<GetOpponentPlayerQueryHandler> logger,
            IRepository<FriendGameRequest> friendGameRepository)
        {
            _logger = logger;
            _friendGameRepository = friendGameRepository;
        }
        public async Task<bool> Handle(GetOpponentPlayerQuery request, CancellationToken cancellationToken) {
            _logger.LogInformation($"{nameof(Handle)} method running in Handler: {nameof(GetOpponentPlayerQueryHandler)}");


            _logger.LogInformation($"{nameof(Handle)} method completed in Handler: {nameof(GetOpponentPlayerQueryHandler)}");
            return true;
        }
    }
}
