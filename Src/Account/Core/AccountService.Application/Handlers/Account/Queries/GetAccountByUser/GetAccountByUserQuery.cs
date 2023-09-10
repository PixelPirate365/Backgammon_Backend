using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Queries.GetAccountByUser {
    public class GetAccountByUserQuery:IRequest<GetAccountByUserResponse>{
        public Guid UserId { get; set; }
    }
}
