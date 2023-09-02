using AccountService.Common.Enums;
using AccountService.Common.Responses;
using MediatR;

namespace AccountService.Application.Handlers.FriendRequests.Commands.AcceptOrRejectFriendRequest {
    public class AcceptOrRejectFriendRequestCommand : IRequest<Response> {
        public FriendRequestStatusEnum Status { get; set; }
        public Guid FriendRequestId { get; set; }
    }
}
