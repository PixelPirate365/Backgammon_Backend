using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Commands.DeleteFriend {
    public class DeleteFriendCommand : IRequest<bool> {
        public Guid Id { get; set; }
    }
}
