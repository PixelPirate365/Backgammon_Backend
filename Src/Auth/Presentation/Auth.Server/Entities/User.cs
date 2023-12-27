using Microsoft.AspNetCore.Identity;
using Auth.Common.Enums;
using Auth.Server.Interfaces;

namespace Auth.Server.Entities {
    public class User : IdentityUser, ICreationAudited, IModificationAudited, ISoftDelete {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int Status { get; set; } = (int)UserStatusEnum.LoggedOut;
        public bool IsDeleted { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
