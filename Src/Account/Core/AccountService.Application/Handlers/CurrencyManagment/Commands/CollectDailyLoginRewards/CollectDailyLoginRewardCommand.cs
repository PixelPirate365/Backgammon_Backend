using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards {
    public class CollectDailyLoginRewardCommand:IRequest<AccountTotalCoinsResponse> {
    }
}
