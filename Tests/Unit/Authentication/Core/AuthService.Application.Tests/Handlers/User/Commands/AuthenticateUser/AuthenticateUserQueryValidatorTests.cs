using AuthService.Application.Handlers.User.Commands.AuthenticateUser;
using FluentValidation.TestHelper;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.AuthenticateUser {
    public class AuthenticateUserCommandValidatorTests {
        private readonly AuthenticateUserCommandValidator _commandValidator;
        public AuthenticateUserCommandValidatorTests() {
            _commandValidator = new AuthenticateUserCommandValidator();
        }
        [Fact]
        public void AuthenticateUserCommand_ValidCommand() {
            //Arrange
            var command = new AuthenticateUserCommand() {
                Email = "test@gmail.com",
                Password = "Testing123",
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert
            Assert.True(result.IsValid);
        }
        [Fact]
        public void AuthenticateUserCommand_InvalidCommand() {
            //Arrange
            var command = new AuthenticateUserCommand() {
                Email = "testemail",
                Password = "Test12345"
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert

            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count > 0);
        }

    }
}
