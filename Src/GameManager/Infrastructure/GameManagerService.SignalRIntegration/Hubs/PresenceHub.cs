using GameManagerService.Common.Utilities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.SignalRIntegration.Hubs {
    public class PresenceHub : Hub {
        readonly ILogger<PresenceHub> _logger;
        readonly GameHubsConnectionUtility _gameHubsConnectionUtility;
        readonly IMediator _mediator;
        public PresenceHub(
            ILogger<PresenceHub> logger,
            GameHubsConnectionUtility gameHubsConnectionUtility,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _gameHubsConnectionUtility = gameHubsConnectionUtility;
        }
        public async override Task OnConnectedAsync() {
           
        }
        public async override Task OnDisconnectedAsync(Exception exception) {
           
        }
    }
}
