using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerService.Application.Handlers.Profile.Commands.DeletePlayer {
    public class DeletePlayerCommand :IRequest<Unit>{
        public Guid UserId { get; set; }
    }
}
