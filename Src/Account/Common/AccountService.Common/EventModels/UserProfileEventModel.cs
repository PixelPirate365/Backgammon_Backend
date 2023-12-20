using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.EventModels {
    public class UserProfileEventModel {
        public Guid UserId { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
}
