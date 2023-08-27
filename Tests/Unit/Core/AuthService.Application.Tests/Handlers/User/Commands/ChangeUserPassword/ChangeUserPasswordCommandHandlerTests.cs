using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using Castle.Core.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.ChangeUserPassword {
    public class ChangeUserPasswordCommandHandlerTests {
        readonly Mock<IIdentityService> _identityService;
        readonly Mock<ICurrentUserService> _currentUserService;
        readonly Mock<ILogger<ChangeUserPasswordCommandHandler>> _logger;
        readonly ChangeUserPasswordCommandHandler _commandHandler = null;
        public ChangeUserPasswordCommandHandlerTests() {
            _identityService = new Mock<IIdentityService>();
            _logger = new Mock<ILogger<ChangeUserPasswordCommandHandler>>();
            _currentUserService = new Mock<ICurrentUserService>();
            _commandHandler = new ChangeUserPasswordCommandHandler(_identityService.Object, _logger.Object, _currentUserService.Object);
        }
        [Fact]
        public async Task ChangeUserPasswordCommandHandler_Successfully() {
            //Arange
            var command = new ChangeUserPasswordCommand() {
                CurrentPassword = "Test123123",
                NewPassword = "Test321321",
                ConfirmNewPassword = "Test321321"
            };
            var testUser = new ApplicationUser {
                Id = "8bcaff90-0af2-4772-a393-cba1c08f5249",
                Email = "test@gmail.com",
                UserName = "TestUser"
            };
            var response = new Response {
                Successful = true,
            };
            _currentUserService.Setup(x => x.UserId).Returns(testUser.Id);
            _identityService.Setup(x => x.FindByIdAsync(testUser.Id)).ReturnsAsync(testUser);
            _identityService.Setup(x => x.CheckPasswordAsync(testUser, command.CurrentPassword)).ReturnsAsync(true);
            _identityService.Setup(x => x.ChangePasswordAsync(testUser, command.CurrentPassword, command.NewPassword)).ReturnsAsync(IdentityResult.Success);
            //Act
            Response result = await _commandHandler.Handle(command, new CancellationToken());
            //Asert
            _logger.VerifyLog(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(2));
            Assert.NotNull(result);
            Assert.True(result.Successful);
        }
        //[Fact]
        //public async Task ChangeUserPasswordCommandHandler_Unsuccessfully() {
        //    //Arange
        //    var query = new AuthenticateUserQuery {
        //        Email = "test@gmail.com",
        //        Password = "Test123123"
        //    };
        //    var testUser = new ApplicationUser {
        //        Email = query.Email,
        //        UserName = "TestUser"
        //    };
        //    var response = new Response<AuthenticationResponse>() {
        //        Successful = false,
        //        Result = new AuthenticationResponse {
        //            Email = testUser.Email,
        //            Username = testUser.UserName,
        //            Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U",
        //            RefreshToken = "B59DBA80-7AC4-42A9-0342-08DBA603F143"
        //        }
        //    };
        //    _identityService.Setup(x => x.FindByEmailAsync(query.Email)).ReturnsAsync(testUser);
        //    _identityService.Setup(x => x.CheckPasswordAsync(testUser, query.Password)).ReturnsAsync(false);
        //   // _tokenService.Setup(x => x.GenerateUserTokenAsync(testUser)).ReturnsAsync(response);
        //    //Act
        //    //var result = await _commandHandler.Handle(query, new CancellationToken());
        //    //Asert
        //    _logger.VerifyLog(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(1));
        //    Assert.NotNull(result);
        //    Assert.False(result.Successful);
        //}

    }
    
}
