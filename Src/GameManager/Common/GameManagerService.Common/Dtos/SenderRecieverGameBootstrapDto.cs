using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Common.Dtos {
    public class SenderRecieverGameBootstrapDto {
        public Guid SenderId { get; set; }
        public Guid RecieverId { get; set; }

    }
}
