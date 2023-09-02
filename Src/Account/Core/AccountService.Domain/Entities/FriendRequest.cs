using AccountService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities {
    public class FriendRequest:BaseEntity, ICreationAudited, IModificationAudited, ISoftDelete {
        public Guid Id { get; set; }
        public Guid SenderProfileId { get; set; }
        public Guid RecieverProfileId { get; set; }
        public int Status { get; set; }
        public virtual AccountProfile SenderProfile { get; set; } = null;
        public virtual AccountProfile RecieverProfile { get; set; } = null;
    }
}
