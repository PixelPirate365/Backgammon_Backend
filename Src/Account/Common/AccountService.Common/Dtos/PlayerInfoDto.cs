using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.Dtos {
    public class PlayerInfoDto {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string PlayerColor { get; set; } = "green";

    }
}
