using GameManagerService.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest {
    public class SendGameRequestCommand :IRequest<Response>{
        public Guid RecieverId { get; set; }
        public bool IsFriendly { get; set; }
        public decimal? BetAmount { get; set; }
    }
}
