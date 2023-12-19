using AccountService.Application.Handlers.CurrencyManagment.Commands.CollectDailyLoginRewards;
using AccountService.Application.Handlers.CurrencyManagment.Commands.UpdateAccountCurrency;
using AccountService.Application.Handlers.CurrencyManagment.Queries.GetTopCurrencyAccountProfiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers {
    //public class CurrencyController : BaseController<CurrencyController> {
    //    private readonly IMediator _mediator;
    //    public CurrencyController(IMediator mediator) {
    //        _mediator = mediator;
    //    }
    //    [HttpPost(nameof(CollectDailyReward))]
    //    public async Task<ActionResult<AccountTotalCoinsResponse>> CollectDailyReward() {
    //        var response = await _mediator.Send(new CollectDailyLoginRewardCommand());
    //        return Ok(response);
    //    }
    //    [HttpGet(nameof(GetTopCurrencyAccountProfiles))]
    //    public async Task<ActionResult<List<GetTopCurrencyAccountProfileResponse>>> GetTopCurrencyAccountProfiles(int maximumNumberOfProfiles) {
    //        var response = await _mediator.Send(
    //            new GetTopCurrencyAccountProfilesQuery() { MaximumNumberOfProfiles = maximumNumberOfProfiles });
    //        return Ok(response);
    //    }
    //    [HttpPost(nameof(UpdateAccountProfileCurrency))]
    //    public async Task<ActionResult<UpdateAccountCurrencyResponse>> UpdateAccountProfileCurrency([FromBody] UpdateAccountCurrencyCommand command) {
    //        var response = await _mediator.Send(command);
    //        return Ok(response);
    //    }
    //}
}
