using FluentValidation;

namespace AuthService.Application.Handlers.User.Commands.ChangeUserPassword {
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand> {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.NewPassword).NotEmpty()
                .MinimumLength(8);

            RuleFor(x => x.ConfirmNewPassword).NotEmpty()
                .MinimumLength(8)
                .Equal(x => x.NewPassword);
        }
    }
}
