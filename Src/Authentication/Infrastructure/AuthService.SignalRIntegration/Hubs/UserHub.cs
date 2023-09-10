using AuthApi.Common.Enums;
using AuthApi.Common.Utilities;
using AuthService.Application.Handlers.User.Commands.ChangeUserStatus;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AuthService.SignalRIntegration.Hubs {
    //[Authorize]
    public class UserHub : Hub {
        readonly ILogger<UserHub> _logger;
        readonly IMediator _mediator;
        public UserHub(
            ILogger<UserHub> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        //public override Task OnConnectedAsync() {
        //    //var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    //if (!string.IsNullOrEmpty(userId)) {
        //    //    await _mediator.Send(new ChangeUserStatusCommand() { UserStatus= UserStatusEnum.LoggedIn});
        //    //    HubConnections.AddUserConnection(userId, Context.ConnectionId);
        //    //}
        //    return base.OnConnectedAsync();
        //}
        //public async override Task OnDisconnectedAsync(Exception? exception) {
        //    //var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    //if (HubConnections.HasUserConnection(userId, Context.ConnectionId)) {
        //    //    var UserConnections = HubConnections.Users[userId];
        //    //    HubConnections.Users.Remove(userId);
        //    //    await _mediator.Send(new ChangeUserStatusCommand() { UserStatus = UserStatusEnum.LoggedOut});
        //    //    if (UserConnections.Any())
        //    //        HubConnections.Users.Add(userId, UserConnections);

        //    //}
        //    //if (!string.IsNullOrEmpty(userId)) {
        //    //    HubConnections.AddUserConnection(userId, Context.ConnectionId);
        //    //}
        //    await base.OnDisconnectedAsync(exception);
        //}


        public override Task OnConnectedAsync()
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                //await _mediator.Send(new ChangeUserStatusCommand() { UserStatus = UserStatusEnum.LoggedIn }).GetAwaiter().GetResult();
                HubConnections.AddUserConnection(userId, Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (HubConnections.HasUserConnection(userId, Context.ConnectionId))
            {
                var UserConnections = HubConnections.Users[userId];
                HubConnections.Users.Remove(userId);
                //await _mediator.Send(new ChangeUserStatusCommand() { UserStatus = UserStatusEnum.LoggedOut });
                if (UserConnections.Any())
                    HubConnections.Users.Add(userId, UserConnections);

            }
            if (!string.IsNullOrEmpty(userId))
            {
                HubConnections.AddUserConnection(userId, Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
