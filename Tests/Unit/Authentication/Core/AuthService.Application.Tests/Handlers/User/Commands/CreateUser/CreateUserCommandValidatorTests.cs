using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Handlers.User.Commands.RefreshUserToken;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.CreateUser {
    public class CreateUserCommandValidatorTests {
        readonly CreateUserCommandValidator _commandValidator;
        public CreateUserCommandValidatorTests()
        {
            _commandValidator = new CreateUserCommandValidator();
        }
        [Fact]
        public void CreateUserCommand_ValidCommand() {
            //Arrange        

            var command = new CreateUserCommand() {
            Email="test@gmail.com",
            UserName="TestUser",
            Password="Test123123"
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert
            Assert.True(result.IsValid);
        }
        [Fact]
        public void CreateUserCommand_InvalidCommand() {
            //Arrange        

            var command = new CreateUserCommand() {
                Email = "test",
                UserName = "TestUser",
                Password = "test123123"
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count > 0);
        }
    }
}
