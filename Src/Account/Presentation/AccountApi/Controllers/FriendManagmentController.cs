using AccountService.Application.Handlers.FriendRequests.Commands.AcceptOrRejectFriendRequest;
using AccountService.Application.Handlers.FriendRequests.Commands.DeleteFriend;
using AccountService.Application.Handlers.FriendRequests.Commands.SendFriendRequest;
using AccountService.Application.Handlers.FriendRequests.Queries.GetFriendsRequest;
using AccountService.Application.Handlers.Friends.Queries.GetFriends;
using AccountService.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AccountApi.Controllers
{
    public class FriendManagmentController : BaseController<FriendManagmentController> {
        private readonly IMediator _mediator;
        public FriendManagmentController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpPost(nameof(SendFriendRequest))]
        public async Task<ActionResult<bool>> SendFriendRequest([FromBody] SendFriendRequestCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete(nameof(DeleteFriend))]
        public async Task<ActionResult<bool>> DeleteFriend([FromBody] DeleteFriendCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost(nameof(AcceptOrRejectFriendRequest))]
        public async Task<ActionResult<Response>> AcceptOrRejectFriendRequest([FromBody] AcceptOrRejectFriendRequestCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpGet(nameof(GetFriendsRequest))]
        public async Task<ActionResult<List<GetFriendRequestResponse>>> GetFriendsRequest() {
            var result = await _mediator.Send(new GetFriendsRequestQuery());
            return Ok(result);
        }
        [HttpGet(nameof(GetFriends))] // Test later
        public async Task<ActionResult<GetFriendResponse>> GetFriends() {
            var result = await _mediator.Send(new GetFriendsQuery());
            return Ok(result);
        }
    }
}
