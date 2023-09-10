using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.Dtos {
    public class AccountInfoDto {
        public List<string> ConnectionIds { get; set; }
        public string Nickname { get; set; }
    }
}
