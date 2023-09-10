using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Friends.Queries.GetOnlineFriends {
    public class GetOnlineFriendsQuery: IRequest<GetOnlineFriendResponse> {
        public List<Guid> UserIds { get; set; }
    }
}
