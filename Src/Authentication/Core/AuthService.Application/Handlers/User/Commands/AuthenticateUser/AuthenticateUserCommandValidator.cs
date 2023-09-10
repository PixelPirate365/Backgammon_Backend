using AuthService.Application.Common.Constants;
using FluentValidation;

namespace AuthService.Application.Handlers.User.Commands.AuthenticateUser {
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand> {
        public AuthenticateUserCommandValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(ValidationConstants.MinimumPasswordLength)
                .MaximumLength(ValidationConstants.MaximumPasswordLength);
        }
    }
}
