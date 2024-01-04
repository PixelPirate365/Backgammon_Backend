using GameManagerService.Domain.Common;

namespace GameManagerService.Domain.Entities {
    public class Player :BaseEntity, IModificationAudited, ICreationAudited, ISoftDelete {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public string PlayerColor { get; set; }
        public int TotalWin { get; set; }
        public int TotalLose { get; set; }
        public ICollection<Game> Games { get; set; }
        public ICollection<FriendGameRequest> SenderFriendGameRequests { get; set; }
        public ICollection<FriendGameRequest> RecieverFriendGameRequests { get; set; }
        public ICollection<MatchMaking> SenderMatchMakingRequests { get; set; }
        public ICollection<MatchMaking> RandomMatchMakingRequests { get; set; }
        public ICollection<GamePlayers> PlayerOneGamePlays { get; set; }
        public ICollection<GamePlayers> PlayerTwoGamePlays { get; set; }




    }
}
