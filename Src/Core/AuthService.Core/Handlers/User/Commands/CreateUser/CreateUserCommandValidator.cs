using AuthService.Application.Common.Constants;
using FluentValidation;

namespace AuthService.Application.Handlers.User.Commands.CreateUser {
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {

        public CreateUserCommandValidator() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserName).NotEmpty()
                .MinimumLength(ValidationConstants.MinimumUserNameLength)
                .MaximumLength(ValidationConstants.MaximumUserNameLength);
            RuleFor(x => x.Password).NotEmpty()
                .MinimumLength(ValidationConstants.MinimumPasswordLength)
                .MaximumLength(ValidationConstants.MinimumPasswordLength);
        }
    }
}
