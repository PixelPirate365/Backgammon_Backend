using AuthService.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Handlers.User.Commands.CreateUser {
    public class CreateUserCommand: IRequest<Response<AuthenticationResponse>> {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
