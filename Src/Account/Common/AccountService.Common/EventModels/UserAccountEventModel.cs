using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.EventModels {
    public class UserAccountEventModel {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
    }
}
