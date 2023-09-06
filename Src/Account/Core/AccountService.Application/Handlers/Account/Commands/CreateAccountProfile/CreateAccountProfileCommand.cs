using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Commands.CreateAccountProfile {
    public class CreateAccountProfileCommand:IRequest<Unit> {
        public Guid UserId { get; set; }
        public string Nickname { get; set; } = null!;
    }
}
