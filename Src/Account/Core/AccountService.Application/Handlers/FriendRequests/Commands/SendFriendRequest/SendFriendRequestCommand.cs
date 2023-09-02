using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.FriendRequests.Commands.SendFriendRequest
{
    public class SendFriendRequestCommand : IRequest<bool>
    {
        public Guid RecieverProfileId { get; set; }
    }
}
