using AccountService.Application.Validators;
using FluentValidation;

namespace AccountService.Application.Handlers.FriendRequests.Commands.SendFriendRequest
{
    public class SendFriendRequestCommandValidator : BaseValidator<SendFriendRequestCommand>
    {
        public SendFriendRequestCommandValidator()
        {
            RuleFor(x => x.RecieverProfileId).NotEmpty().Must(Validate);
        }
    }
}
