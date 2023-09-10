using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Friends.Queries.GetOnlineFriends {
    public class GetOnlineFriendResponse {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string UserName { get; set; }
    }
}
