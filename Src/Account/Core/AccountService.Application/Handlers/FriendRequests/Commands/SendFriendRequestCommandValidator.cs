using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Commands {
    public class SendFriendRequestCommandValidator:AbstractValidator<SendFriendRequestCommand> {
        public SendFriendRequestCommandValidator() {
            RuleFor(x=> x.RecieverId).NotEmpty().Must(Validate);
        }
        protected static bool Validate(Guid identifier) => identifier != Guid.Empty;
    }
}
