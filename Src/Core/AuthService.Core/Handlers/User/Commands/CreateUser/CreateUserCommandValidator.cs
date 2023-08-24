using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application.Handlers.User.Commands.CreateUser {
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {

        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(8); //hard coded
        }
    }
}
