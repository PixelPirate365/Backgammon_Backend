using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Friends.Queries.GetFriends {
    public class GetFriendsQuery: IRequest<GetFriendResponse> {
        public List<Guid> UserIds { get; set; }
    }
}
