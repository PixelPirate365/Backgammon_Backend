using AccountService.Application.Validators;
using FluentValidation;

namespace AccountService.Application.Handlers.FriendRequests.Commands.DeleteFriend {
    public class DeleteFriendCommandValidator : BaseValidator<DeleteFriendCommand> {
        public DeleteFriendCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().Must(Validate);
        }
    }
}
