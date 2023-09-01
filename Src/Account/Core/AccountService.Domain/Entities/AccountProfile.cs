using AccountService.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities {
    public class AccountProfile:BaseEntity {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? Nickname { get; set; }
        public string? ProfileDescription { get; set; }
        public DateTime? BirthDate { get; set; } 
        public GenderEnum Gender { get; set; } = GenderEnum.Unkown;
        public string? Image { get; set; }
        public ICollection<FriendRequest> SendFriendRequests { get; set; } = null;
        public ICollection<FriendRequest> RecieveFriendRequests { get; set; } = null;

    }
}
