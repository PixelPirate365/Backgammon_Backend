using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Application.Handlers.User.Commands.DeleteUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using AuthService.MessageBus.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.DeleteUser
{
    public class DeleteUserCommandHandlerTests {
        readonly Mock<IIdentityService> _identityService;
        readonly Mock<ICurrentUserService> _currentUserService;
        readonly Mock<IRepository<ApplicationUser>> _userRepository;
        readonly Mock<ILogger<DeleteUserCommandHandler>> _logger;
        readonly Mock<IRabbitMQMessageSender> _messageSender;
        readonly DeleteUserCommandHandler _commandHandler;
        public DeleteUserCommandHandlerTests() {
            _identityService = new Mock<IIdentityService>();
            _currentUserService = new Mock<ICurrentUserService>();
            _userRepository = new Mock<IRepository<ApplicationUser>>();
            _logger = new Mock<ILogger<DeleteUserCommandHandler>>();
            _messageSender = new Mock<IRabbitMQMessageSender>();
            _commandHandler = new DeleteUserCommandHandler(_currentUserService.Object, _identityService.Object, _userRepository.Object, _logger.Object, _messageSender.Object);
        }
        [Fact]
        public async Task DeleteUserCommandHandler_Successfully() {
            //Arrange
            var testUser = new ApplicationUser {
                Id = "8bcaff90-0af2-4772-a393-cba1c08f5249",
                Email = "test@gmail.com",
                UserName = "TestUser"
            };
            var response = new Response {
                Successful = true
            };
            var command = new DeleteUserCommand();
            _currentUserService.Setup(x => x.UserId).Returns(testUser.Id);
            _identityService.Setup(x => x.FindByIdAsync(testUser.Id)).ReturnsAsync(testUser);
            _userRepository.Setup(x => x.Delete(testUser)).ReturnsAsync(true);
            //Act
            var result = await _commandHandler.Handle(command, new CancellationToken());
            //Asert
            _logger.VerifyLog(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(2));
            Assert.True(result.Successful);
        }
        [Fact]
        public async Task DeleteUserCommandHandler_UnSuccessfully() {
            //Arrange
            var command = new DeleteUserCommand();
            _currentUserService.Setup(x => x.UserId).Throws(new NullReferenceException());
            //Act
            var result = async () => await _commandHandler.Handle(command, new CancellationToken());
            var exception = await Assert.ThrowsAsync<NullReferenceException>(result);
            //Assert
            Assert.NotNull(exception);
        }
    }
}
