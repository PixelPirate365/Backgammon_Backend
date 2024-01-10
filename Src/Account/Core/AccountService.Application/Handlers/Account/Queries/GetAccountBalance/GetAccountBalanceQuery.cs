using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.Account.Queries.GetAccountBalance {
    public class GetAccountBalanceQuery : IRequest<Unit> {
        public Guid RecieverId { get; set; }

        public Guid SenderId { get; set; }

        public decimal BetAmount { get; set; }

    }
}
