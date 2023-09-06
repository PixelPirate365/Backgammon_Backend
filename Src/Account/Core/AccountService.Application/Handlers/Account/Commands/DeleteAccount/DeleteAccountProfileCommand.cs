using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Commands.DeleteAccount {
    public class DeleteAccountProfileCommand:IRequest<bool> {
        public Guid UserId { get; set; }
    }
}
