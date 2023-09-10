using GameManagerService.Domain.Common;

namespace GameManagerService.Domain.Entities {
    public class Player : BaseAuditEntity,IModificationAudited,ICreationAudited {
        public Guid Id { get; set; }
        public Guid? AccountId { get; set; }
        public int PlayerColor { get; set; }
        public int TotalWin { get; set; }
        public int TotalLose { get; set; }
        public ICollection<Game> Games { get; set; }
    }
}
