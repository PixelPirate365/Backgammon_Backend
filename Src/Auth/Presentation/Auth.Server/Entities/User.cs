using Microsoft.AspNetCore.Identity;
using Auth.Common.Enums;

namespace Auth.Server.Entities {
    public class User : IdentityUser {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public int Status { get; set; } = (int)UserStatusEnum.LoggedOut;
        //public bool IsDeleted { get; set; }
        //public string? CreatedBy { get; set; }
        //public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public string? ModifiedBy { get; set; }
        //public DateTime? ModifiedAt { get; set; }
    }
}
