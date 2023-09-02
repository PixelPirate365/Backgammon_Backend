using AccountService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities {
    public class AccountProfileCurrency : BaseEntity, ICreationAudited, IModificationAudited, ISoftDelete {
        public Guid Id { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid AccountProfileId { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual AccountProfile AccountProfile { get; set; }
    }
}
