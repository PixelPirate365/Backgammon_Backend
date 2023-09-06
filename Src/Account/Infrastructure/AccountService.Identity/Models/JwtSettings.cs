using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Identity.Models {
    public class JwtSettings {
        public string Secret { get; set; } = null!;
    }
}
