using AuthService.Common.Responses;
using MediatR;

namespace AuthService.Application.Handlers.User.Commands.AuthenticateUser {
    public class AuthenticateUserCommand : IRequest<Response<AuthenticationResponse>> {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
