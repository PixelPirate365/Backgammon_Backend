using GameManagerService.Domain.Common;

namespace GameManagerService.Domain.Entities {
    public class Game:BaseEntity,IModificationAudited,ICreationAudited {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public GamePlayers GamePlayers { get; set; }
        public int BetAmount { get; set; }
        public ICollection<Move> Moves { get; set; }

    }
}
