using GameManagerService.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Entities {
    public class GamePlayers:BaseEntity,ICreationAudited,IModificationAudited {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerOneId { get; set; }
        public Guid PlayerTwoId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player PlayerOne { get; set;}
        public virtual Player PlayerTwo { get; set; }

    }
}
