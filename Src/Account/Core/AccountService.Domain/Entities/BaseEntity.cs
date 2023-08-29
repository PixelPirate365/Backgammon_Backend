using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Domain.Entities {
    public class BaseEntity : BaseAuditEntity {
        public bool IsDeleted { get; set; } = false;
    }
}
