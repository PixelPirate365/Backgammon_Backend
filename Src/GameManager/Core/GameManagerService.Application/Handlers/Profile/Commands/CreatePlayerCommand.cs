using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Handlers.Profile.Commands {
    public class CreatePlayerCommand: IRequest<Unit> {
        public Guid UserId { get; set; }
        public Guid AccountId { get; set; }
        public string PlayerColor { get; set; }
    }
}
