using GameManagerService.Application.Handlers.FriendGame.Commands.SendGameRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameManagerApi.Controllers {
    public class FriendGameRequestController :BaseController<FriendGameRequestController> {
        readonly IMediator _mediator;
        public FriendGameRequestController(
            IMediator mediator) {

            _mediator = mediator;
        }
        [HttpPost(nameof(SendGameRequest))]
        public async Task<ActionResult<bool>> SendGameRequest([FromBody] SendGameRequestCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
