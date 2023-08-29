using AuthService.Common.Responses;
using MediatR;

namespace AuthService.Application.Handlers.User.Queries.AuthenticateUser {
    public class AuthenticateUserQuery : IRequest<Response<AuthenticationResponse>> {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
