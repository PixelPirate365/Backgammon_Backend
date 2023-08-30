using AccountService.Application.Handlers.Account.Queries;
using AccountService.Common.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Commands {
    public class UpdateAccountProfileCommand : IRequest<UpdateAccountCommandResponse> {
        public string? Nickname { get; set; }
        public string? ProfileDescription { get; set; }
        public string? Image { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum Gender { get; set; }
    }
}
