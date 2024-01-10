using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Common.Constants {
    public static class EventNameConstants {
        public const string PlayerCreationEvent = nameof(PlayerCreationEvent);
        public const string PlayerDeletionEvent = nameof(PlayerDeletionEvent);
        public const string CheckRecieverBalanceEvent = nameof(CheckRecieverBalanceEvent);
        public const string SendRecieverBalanceEvent = nameof(SendRecieverBalanceEvent);
    }
}
