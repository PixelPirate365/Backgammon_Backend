using GameManagerService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Entities {
    public class GameState:BaseEntity,ICreationAudited,IModificationAudited {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid CurrentPlayerId { get; set; }
        public string BoardState { get; set; }
        public virtual Player CurrentPlayer { get; set; }
        public virtual Game Game { get; set; }
    }
}
