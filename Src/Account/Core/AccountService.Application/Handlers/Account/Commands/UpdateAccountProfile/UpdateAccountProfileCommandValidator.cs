using AccountService.Application.Validators;
using FluentValidation;

namespace AccountService.Application.Handlers.Account.Commands.UpdateAccountProfile
{
    public class UpdateAccountProfileCommandValidator : BaseValidator<UpdateAccountResponse> {
        public UpdateAccountProfileCommandValidator() {
            RuleFor(x => x.Nickname)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Nickname is required.")
                .MinimumLength(3).WithMessage("Nickname must be at least 3 characters long.");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image is required.")
                .Must(IsValidUri).WithMessage("Image must be a valid URI.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Birth date is required.")
                .LessThan(DateTime.UtcNow.AddYears(-16)).WithMessage("You must be at least 16 years old.");

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required.")
                .IsInEnum().WithMessage("Invalid gender value.");
        }

        private bool IsValidUri(string uri) {
            return Uri.TryCreate(uri, UriKind.Absolute, out _);
        }
    }
}
