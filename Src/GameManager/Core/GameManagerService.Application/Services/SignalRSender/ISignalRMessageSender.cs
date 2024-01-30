using GameManagerService.Common.Dtos;
using GameManagerService.Common.Responses;

namespace GameManagerService.Application.Services.SignalRSender {
    public interface ISignalRMessageSender {
        Task NotifySenderRecieverBalanceNotAvailable(Response<ParentMessageDto<bool>> parentMessage);
        Task NotifySenderRecieverBalanceAvailable(Response<ParentMessageDto<SenderRecieverGameBootstrapDto>> parentMessage);



    }
}
