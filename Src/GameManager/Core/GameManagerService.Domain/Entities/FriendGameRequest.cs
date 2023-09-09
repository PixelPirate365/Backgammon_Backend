using GameManagerService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Entities {
    public class FriendGameRequest : BaseEntity, ICreationAudited, IModificationAudited {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecieverId { get; set; }
        public int Status { get; set; } 
    }
}
