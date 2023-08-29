using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using FluentValidation.TestHelper;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.ChangeUserPassword
{
    public class ChangeUserPasswordCommandValidatorTests {
        readonly ChangeUserPasswordCommandValidator _commandValidator;
        public ChangeUserPasswordCommandValidatorTests()
        {
            _commandValidator = new ChangeUserPasswordCommandValidator();
        }
        [Fact]
        public void ChangeUserPasswordCommand_ValidCommand() {
            //Arrange        

            var command = new ChangeUserPasswordCommand() {
                CurrentPassword = "Test1231",
                NewPassword = "Test3213",
                ConfirmNewPassword = "Test3213"
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert
            Assert.True(result.IsValid);
        }
        [Fact]
        public void ChangeUserPasswordCommand_InvalidCommand() {
            //Arrange        

            var command = new ChangeUserPasswordCommand() {
                CurrentPassword = "Test123123",
                NewPassword = "test321321",
                ConfirmNewPassword = "Test321321"
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count > 0);
        }
    }
}
