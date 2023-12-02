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
        [HttpPost(nameof(SendFriendRequest))]
        public async Task<ActionResult<bool>> SendFriendRequest([FromBody] SendGameRequestCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
