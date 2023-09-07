using AuthService.Application.Common.Constants;
using FluentValidation;

namespace AuthService.Application.Handlers.User.Commands.ChangeUserPassword {
    public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand> {
        public ChangeUserPasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty()
                .MinimumLength(ValidationConstants.MinimumPasswordLength)
                .MaximumLength(ValidationConstants.MinimumPasswordLength);

            RuleFor(x => x.NewPassword).NotEmpty()
                .MinimumLength(ValidationConstants.MinimumPasswordLength)
                .MaximumLength(ValidationConstants.MinimumPasswordLength);

            RuleFor(x => x.ConfirmNewPassword).NotEmpty()
                .MinimumLength(ValidationConstants.MinimumPasswordLength)
                .MaximumLength(ValidationConstants.MinimumPasswordLength);
        }
    }
}
