using AccountService.Application.Common.Extensions;
using AccountService.Application.Handlers.Account.Queries.GetAccountByUser;
using AccountService.Common.Utilities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Principal;

namespace AccountService.SignalRIntegration.Hubs {
    public class PresenceHub : Hub {

        readonly ILogger<PresenceHub> _logger;
        readonly PresenceUtility _presenceUtility;
        readonly IMediator _mediator;
        public PresenceHub(
            ILogger<PresenceHub> logger,
            PresenceUtility presenceUtility,
            IMediator mediator) {
            _logger = logger;
            _presenceUtility = presenceUtility;
            _mediator = mediator;
        }
        public async override Task OnConnectedAsync() {
            await _presenceUtility.AccountConnected(Context.User.GetUserId(), Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception) {
            await _presenceUtility.AccountDisconnected(Context.User.GetUserId(), Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
