using AccountService.Application.Handlers.Account.Commands.UpdateAccountProfile;
using AccountService.Application.Handlers.Account.Queries.GetAccountProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers
{
    public class AccountController : BaseController<AccountController> {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet(nameof(GetAccountProfile))]
        public async Task<ActionResult<GetAccountProfileResponse>> GetAccountProfile() {
            var result = await _mediator.Send(new GetAccountProfileQuery());
            return Ok(result);
        }
        [HttpPost(nameof(UpdateAccountProfile))]
        public async Task<ActionResult<UpdateAccountResponse>> UpdateAccountProfile([FromBody] UpdateAccountProfileCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
