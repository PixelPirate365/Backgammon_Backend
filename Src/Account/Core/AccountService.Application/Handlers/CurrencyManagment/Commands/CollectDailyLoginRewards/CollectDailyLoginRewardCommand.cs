using MediatR;

namespace AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards {
    public class CollectDailyLoginRewardCommand : IRequest<AccountTotalCoinsResponse> {
    }
}
