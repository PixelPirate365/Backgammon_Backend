using AccountService.Application.Validators;
using FluentValidation;

namespace AccountService.Application.Handlers.FriendRequests.Commands.DeleteFriend {
    public class DeleteFriendCommandValidator : BaseValidator<DeleteFriendCommand> {
        public DeleteFriendCommandValidator() {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("The friend identifier should not be empty.")
                .Must(Validate).WithMessage("The friend identifier must be valid.");
        }
    }
}
