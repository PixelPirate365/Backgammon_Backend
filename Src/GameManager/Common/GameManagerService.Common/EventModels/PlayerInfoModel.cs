namespace GameManagerService.Common.EventModels {
    public class PlayerInfoModel {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string PlayerColor { get; set; }
    }
}
