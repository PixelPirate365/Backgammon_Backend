using AccountService.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Queries.GetFriendsRequest {
    public class GetFriendRequestResponse{
        public Guid Id { get; set; }
        public string SenderName { get; set; }
    }
}
