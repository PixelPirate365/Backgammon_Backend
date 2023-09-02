using AccountService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities {
    public class Currency : BaseEntity, ICreationAudited, IModificationAudited, ISoftDelete { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AccountProfileCurrency> AccountProfileCurrencies { get; set; }

    }
}
