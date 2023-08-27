using AuthService.Application.Handlers.User.Commands.RefreshUserToken;
using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.RefreshUserToken {
    public class RefreshUserTokenCommandValidatorTests {
        private readonly RefreshUserTokenCommandValidator _commandValidator;
        public RefreshUserTokenCommandValidatorTests()
        {
            _commandValidator = new RefreshUserTokenCommandValidator();
        }
        [Fact]
        public void RefreshTokenUserCommand_ValidCommand() {
            //Arrange        

            var command = new RefreshUserTokenCommand() {
                Token= "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U",
                RefreshToken = "EE59142A-F61C-4818-0341-08DBA603F143"
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert
            Assert.True(result.IsValid);
        }
        [Fact]
        public void RefreshTokenUserCommand_InvalidCommand() {
            //Arrange
            var command = new RefreshUserTokenCommand() {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U"               
            };
            //Act
            var result = _commandValidator.TestValidate(command);
            //Assert
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count > 0);
        }
    }
}
