using FluentValidation;
using GameManagerService.Application.Validators;

namespace GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest {
    public class SendGameRequestValidator : BaseValidator<SendGameRequestCommand> {
        public SendGameRequestValidator() {
            RuleFor(x => x.RecieverId).Must(Validate);
            RuleFor(x => x.BetAmount).Must(ValidateAmount).When(
                x => !x.IsFriendly);
        }
    }
}
