using AuthApi.Common.Enums;
using MediatR;

namespace AuthService.Application.Handlers.User.Commands.ChangeUserStatus {
    public class ChangeUserStatusCommand : IRequest<UserStatusEnum> {
        public UserStatusEnum UserStatus { get; set; }
    }
}
