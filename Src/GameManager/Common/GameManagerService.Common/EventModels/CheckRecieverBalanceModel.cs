using System.Text.Json.Serialization;

namespace GameManagerService.Common.EventModels {
    public class CheckRecieverBalanceModel {
        public Guid RecieverId { get; set; }

        public Guid? SenderId { get; set; }

        public decimal BetAmount { get; set; }
    }
}
