using AccountService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities {
    public class AccountDailyReward : BaseAuditEntity, ICreationAudited, IModificationAudited {
        public Guid Id { get; set; }
        public Guid AccountProfileId { get; set; }
        public virtual AccountProfile AccountProfile { get; set; }


    }
    
}
