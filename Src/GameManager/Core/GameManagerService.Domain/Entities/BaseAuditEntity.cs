using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Entities {
    public class BaseAuditEntity {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [StringLength(450)]
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        [StringLength(450)]
        public string? ModifiedBy { get; set; }
    }
}
