using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Handlers.User.Commands.DeleteUser;
using AuthService.Application.Handlers.User.Commands.RefreshUserToken;
using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers {
    public class UserController : BaseController<UserController> {
        readonly IMediator _mediator;
        readonly ICurrentUserService _currentUserService;
        public UserController(IMediator mediator,
            ICurrentUserService currentUserService) {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [AllowAnonymous]
        [HttpPost(nameof(CreateUser))]
        public async Task<ActionResult<Response<AuthenticationResponse>>> CreateUser([FromBody] CreateUserCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(nameof(ChangePassword))]
        public async Task<ActionResult<Response>> ChangePassword([FromBody] ChangeUserPasswordCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost(nameof(Login))]
        public async Task<ActionResult<Response<AuthenticationResponse>>> Login([FromBody] AuthenticateUserQuery query) {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost(nameof(RefreshToken))]
        public async Task<ActionResult<Response<AuthenticationResponse>>> RefreshToken([FromBody] RefreshUserTokenCommand command) {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete(nameof(DeleteUser))]
        public async Task<ActionResult<Response>> DeleteUser() {
            var result = await _mediator.Send(new DeleteUserCommand());
            return Ok(result);
        }
        [HttpGet(nameof(ValidateToken))]
        public async Task<ActionResult<bool>> ValidateToken() {
            if (!string.IsNullOrWhiteSpace(_currentUserService.UserId)) {
                await Task.Delay(0);
                return Ok(true);
            }
            return Ok(false);
        }
    }
}
