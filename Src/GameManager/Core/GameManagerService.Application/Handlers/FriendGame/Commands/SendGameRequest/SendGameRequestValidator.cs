using FluentValidation;
using GameManagerService.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest {
    public class SendGameRequestValidator:BaseValidator<SendGameRequestCommand> {
        public SendGameRequestValidator()
        {
            RuleFor(x => x.RecieverId).Must(Validate);
        }
    }
}
