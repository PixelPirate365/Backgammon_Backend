using GameManagerService.Common.Dtos;
using GameManagerService.Common.Responses;
using GameManagerService.SignalRIntegration.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata;

namespace GameManagerService.Application.Services.SignalRSender {
    public class SignalRMessageSender : ISignalRMessageSender {
        readonly ILogger<SignalRMessageSender> _logger;
        readonly IHubContext<PresenceHub> _presenceHub;
        public SignalRMessageSender(
            ILogger<SignalRMessageSender> logger,
            IHubContext<PresenceHub> presenceHub) {
            _logger = logger;
            _presenceHub = presenceHub;
        }

        public async Task NotifySenderRecieverBalanceAvailable(Response<ParentMessageDto<SenderRecieverGameBootstrapDto>> parentMessage) {
            _logger.LogInformation(
                $"{nameof(Handle)} method running in Handler: {nameof(NotifySenderRecieverBalanceAvailable)}");
            var users = new string[] {
               parentMessage.Result.Message.RecieverId.ToString(),
               parentMessage.Result.Message.SenderId.ToString(),
           };
            await _presenceHub.Clients.Users(users)
                .SendAsync("NotifySenderRecieverBalanceAvailable", parentMessage.Result); //add as constant later

            _logger.LogInformation(
                $"{nameof(Handle)} method completed in Handler: {nameof(NotifySenderRecieverBalanceAvailable)}");
        }

        public async Task NotifySenderRecieverBalanceNotAvailable(Response<ParentMessageDto<bool>> parentMessage) {
            _logger.LogInformation(
                $"{nameof(Handle)} method running in Handler: {nameof(NotifySenderRecieverBalanceNotAvailable)}");
            await _presenceHub.Clients.User(parentMessage.Result.UserId.ToString())
                .SendAsync("NotifySenderRecieverBalanceNotAvailable", parentMessage.Result); //add as constant later

            _logger.LogInformation(
                $"{nameof(Handle)} method completed in Handler: {nameof(NotifySenderRecieverBalanceNotAvailable)}");
        }
    }
}
