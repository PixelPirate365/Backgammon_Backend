using AccountService.Application.Common.Extensions;
using AccountService.Application.Handlers.Friends.Queries.GetFriends;
using AccountService.Common.Constants;
using AccountService.Common.Utilities;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace AccountService.SignalRIntegration.Hubs {
    public class PresenceHub : Hub {

        readonly ILogger<PresenceHub> _logger;
        readonly IMediator _mediator;
        public PresenceHub(
            ILogger<PresenceHub> logger,
            IMediator mediator) {
            _logger = logger;
            _mediator = mediator;
        }
        public async override Task OnConnectedAsync() {
            await PresenceUtility.AccountConnected(Context.User.GetUserId(), Context.ConnectionId);
            var friends = await _mediator.Send(new GetFriendsQuery());
            var loggedInFriends = PresenceUtility.GetOnlineAccounts().Where(x =>
            friends.OnlineFriendIds.Contains(Guid.Parse(x.Key)));
            await Clients.Users(loggedInFriends.Select(x => x.Key.ToString()).ToList())
                .SendAsync(SignalRHubConstants.AccountIsOnlineEvent, friends.GetLoggedInProfile);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception) {
            await PresenceUtility.AccountDisconnected(Context.User.GetUserId(), Context.ConnectionId);
            var friends = await _mediator.Send(new GetFriendsQuery());
            var loggedInFriends = PresenceUtility.GetOnlineAccounts().Where(x =>
           friends.OnlineFriendIds.Contains(Guid.Parse(x.Key)));
            await Clients.Users(loggedInFriends.Select(x => x.Key.ToString()).ToList())
                .SendAsync(SignalRHubConstants.AccountIsOfflineEvent, friends.GetLoggedInProfile);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
