using GameManagerService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Entities {
    public class Move:BaseEntity,IModificationAudited,ICreationAudited {
        public int Id { get; set; }
        public Guid GamePlayersId { get; set; }
        public int FromPoint { get;set; }
        public int ToPoint { get; set; }
        public int DiceRollOne { get; set; }
        public int DiceRollTwo { get; set; }
        public virtual GamePlayers GamePlayers { get; set; }

    }
}
