using AuthService.Common.Responses;
using MediatR;

namespace AuthService.Application.Handlers.User.Commands.ChangeUserPassword {
    public class ChangeUserPasswordCommand : IRequest<Response> {
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? ConfirmNewPassword { get; set; }
    }
}
