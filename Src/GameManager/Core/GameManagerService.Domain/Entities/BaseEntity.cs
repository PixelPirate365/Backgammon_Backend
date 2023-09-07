using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Entities {
    public class BaseEntity : BaseAuditEntity {
        public bool IsDeleted { get; set; } = false;
    }
}
