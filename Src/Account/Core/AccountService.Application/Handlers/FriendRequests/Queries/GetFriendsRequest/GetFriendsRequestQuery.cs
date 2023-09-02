using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Queries.GetFriendsRequest {
    public class GetFriendsRequestQuery:IRequest<List<GetFriendRequestResponse>> {
    }
}
