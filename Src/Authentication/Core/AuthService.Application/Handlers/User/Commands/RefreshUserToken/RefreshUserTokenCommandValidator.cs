using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Handlers.User.Commands.RefreshUserToken {
    public class RefreshUserTokenCommandValidator: AbstractValidator<RefreshUserTokenCommand> {
        public RefreshUserTokenCommandValidator()
        {
            RuleFor(x => x.Token).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();      
        }
    }
}
