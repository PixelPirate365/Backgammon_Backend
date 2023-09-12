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
            var friends = await _mediator.Send(new GetFriendsQuery());
            var loggedInFriends = _presenceUtility.GetOnlineAccounts().Where(x =>
            friends.OnlineFriendIds.Contains(Guid.Parse(x.Key)));
            await Clients.Users(loggedInFriends.Select(x => x.Key.ToString()).ToList())
                .SendAsync(EventNameConstants.AccountIsOnlineEvent, friends.GetLoggedInProfile);
            await base.OnConnectedAsync();
        }
        public async override Task OnDisconnectedAsync(Exception exception) {
            await _presenceUtility.AccountDisconnected(Context.User.GetUserId(), Context.ConnectionId);
            var friends = await _mediator.Send(new GetFriendsQuery());
            var loggedInFriends = _presenceUtility.GetOnlineAccounts().Where(x =>
           friends.OnlineFriendIds.Contains(Guid.Parse(x.Key)));
            await Clients.Users(loggedInFriends.Select(x => x.Key.ToString()).ToList())
                .SendAsync(EventNameConstants.AccountIsOfflineEvent, friends.GetLoggedInProfile);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
