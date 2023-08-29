using AuthService.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Handlers.User.Commands.RefreshUserToken {
    public class RefreshUserTokenCommand: IRequest<Response<AuthenticationResponse>> {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
