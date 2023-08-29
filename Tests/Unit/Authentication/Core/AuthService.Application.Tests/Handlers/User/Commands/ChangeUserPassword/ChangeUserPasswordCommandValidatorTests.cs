using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using AuthService.Application.Handlers.User.Commands.CreateUser;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.ChangeUserPassword {
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
                CurrentPassword = "Test123123",
                NewPassword = "Test321321",
                ConfirmNewPassword = "Test321321"
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
