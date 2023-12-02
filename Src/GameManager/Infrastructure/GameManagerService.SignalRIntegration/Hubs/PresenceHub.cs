using GameManagerService.Common.Utilities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.SignalRIntegration.Hubs {
    public class PresenceHub : Hub {
        readonly ILogger<PresenceHub> _logger;
        readonly IMediator _mediator;
        public PresenceHub(
            ILogger<PresenceHub> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async override Task OnConnectedAsync() {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId)) {
                await GameHubsConnectionUtility.GameHubsConnected(userId, Context.ConnectionId);
            }
        }
        public async override Task OnDisconnectedAsync(Exception exception) {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (GameHubsConnectionUtility.HasUserConnections(userId,Context.ConnectionId))
            {
                var userConnections = GameHubsConnectionUtility.OnlineUsers[userId];
                userConnections.Remove(Context.ConnectionId);
                GameHubsConnectionUtility.OnlineUsers.Remove(userId);
                if (userConnections.Any()){
                    GameHubsConnectionUtility.OnlineUsers.Add(userId, userConnections);
                }
            }
        }
    }
}
