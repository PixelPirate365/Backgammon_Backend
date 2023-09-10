using AuthApi.Common.Enums;
using AuthService.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Domain.Entities {
    public class ApplicationUser : IdentityUser, ICreationAudited, IModificationAudited, ISoftDelete {
        public int Status { get; set; } = (int)UserStatusEnum.LoggedOut;
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
