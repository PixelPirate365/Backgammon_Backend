using AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers {
    public class CurrencyController:BaseController<CurrencyController> {
        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpPost(nameof(CollectDailyReward))]
        public async Task<ActionResult<AccountTotalCoinsResponse>> CollectDailyReward() {
            var response = await _mediator.Send(new CollectDailyLoginRewardCommand());
            return Ok(response);
        }
    }
}
