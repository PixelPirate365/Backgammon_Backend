using AuthApi.Controllers;
using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Handlers.User.Commands.DeleteUser;
using AuthService.Application.Handlers.User.Commands.RefreshUserToken;
using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using AuthService.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AuthApi.Tests.Controllers
{
    public class UserControllerTests {
        private readonly UserController _controller;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ICurrentUserService> _currentUserService;

        public UserControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _currentUserService = new Mock<ICurrentUserService>();
            _controller = new(_mediator.Object, _currentUserService.Object);
        }
        [Fact]
        public async Task CreateUser_Successfully() {
            //Arrange
            var command = new CreateUserCommand();
            //Act
            var result = await _controller.CreateUser(command);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task ChangePassword_Successfully() {
            //Arange
            var command = new ChangeUserPasswordCommand();
            //Act
            var result = await _controller.ChangePassword(command);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task Login_Successfully() {
            //Arange
            var query = new AuthenticateUserQuery();
            //Act
            var result = await _controller.Login(query);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task DeleteUser_Successfully() {

            //Arrange
            var result = await _controller.DeleteUser();
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task RefreshToken_Successfully() {
            //Arange
            var command = new RefreshUserTokenCommand();
            //Act
            var result = await _controller.RefreshToken(command);
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        [Fact]
        public async Task CreateUser_Unsuccessfully() {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());
            //Act
            var result = async () => await _controller.CreateUser(new CreateUserCommand());
            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(result);
            Assert.NotNull(exception);
        }
        [Fact]
        public async Task ChangePassword_Unsuccessfully() {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<ChangeUserPasswordCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());
            //Act
            var result = async () => await _controller.ChangePassword(new ChangeUserPasswordCommand());
            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(result);
            Assert.NotNull(exception);
        }
        [Fact]
        public async Task Login_Unsuccessfully() {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<AuthenticateUserQuery>(), It.IsAny<CancellationToken>())).Throws(new Exception());
            //Act
            var result = async () => await _controller.Login(new AuthenticateUserQuery());
            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(result);
            Assert.NotNull(exception);
        }
        [Fact]
        public async Task DeleteUser_Unsuccessfully() {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<DeleteUserCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());
            //Act
            var result = async()=> await _controller.DeleteUser();
            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(result);
            Assert.NotNull(exception);
        }
        [Fact]
        public async Task RefreshToken_Unsuccessfully() {
            //Arrange
            _mediator.Setup(x => x.Send(It.IsAny<RefreshUserTokenCommand>(), It.IsAny<CancellationToken>())).Throws(new Exception());
            //Act
            var result = async () => await _controller.RefreshToken(new RefreshUserTokenCommand());
            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(result);
            Assert.NotNull(exception);
        }
    }
}
