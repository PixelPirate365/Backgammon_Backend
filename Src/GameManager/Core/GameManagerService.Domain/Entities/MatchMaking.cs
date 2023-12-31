﻿using GameManagerService.Domain.Common;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Domain.Entities {
    public class MatchMaking:BaseEntity,ICreationAudited,IModificationAudited {
        public Guid Id { get; set; }
        public Guid PlayerSenderId { get; set; }
        public Guid RandomMatchMakerId { get; set; }
        public int Status { get; set; }
        public virtual Player PlayerSender { get; set; }
        public virtual Player RandomMatchMaker { get; set; }

    }
}
