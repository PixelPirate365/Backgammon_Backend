using AccountService.Application.Validators;
using AccountService.Common.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Commands.AcceptOrRejectFriendRequest {
    public class AcceptOrRejectFriendRequestCommandValidator:BaseValidator<AcceptOrRejectFriendRequestCommand> {
        public AcceptOrRejectFriendRequestCommandValidator()
        {
            RuleFor(x => x.FriendRequestId).Must(Validate);
            RuleFor(x => x.Status)
            .IsInEnum()
            .Must(status => status == FriendRequestStatusEnum.Accepted || status == FriendRequestStatusEnum.Rejected)
            .WithMessage("Status must be either Accepted or Rejected.");

        }
    }
}
