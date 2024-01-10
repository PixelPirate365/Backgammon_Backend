using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Common.EventModels {
    public class CheckRecieverBalanceModel {
        public Guid RecieverId { get; set; }

        public Guid SenderId { get; set; }

        public decimal BetAmount { get; set; }

    }
}
