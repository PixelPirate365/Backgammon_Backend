namespace AccountService.Common.EventModels {
    public class SendRecieverBalanceModel {
        public Guid RecieverId { get; set; }
        public Guid SenderId { get; set; }
        public decimal BalanceAmount { get; set; }
        public decimal BetAmount { get; set; }
    }
}
