using AuthService.Domain.Common;

namespace AuthService.Domain.Entities {
    public class BaseEntity : BaseAuditEntity {
        public bool IsDeleted { get; set; } = false;

    }
}
