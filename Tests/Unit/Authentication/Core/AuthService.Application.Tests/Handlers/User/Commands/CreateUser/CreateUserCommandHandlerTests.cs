using AuthService.Application.Handlers.User.Commands.ChangeUserPassword;
using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Interfaces;
using AuthService.Application.Mappings;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.CreateUser {
    public class CreateUserCommandHandlerTests {
        readonly Mock<IIdentityService> _identityService;
        readonly Mock<ITokenService> _tokenService;
        readonly Mock<ILogger<CreateUserCommandHandler>> _logger;
        private readonly IMapper _mapper;
        readonly CreateUserCommandHandler _commandHandler = null;
        public CreateUserCommandHandlerTests()
        {
            _identityService = new Mock<IIdentityService>();
            _logger = new Mock<ILogger<CreateUserCommandHandler>>();
            _tokenService = new Mock<ITokenService>();
            _mapper = new Mapper(new MapperConfiguration(configurations => configurations.AddProfile(MapperConfigurationProfile.UserMappingProfile())));
            _commandHandler = new CreateUserCommandHandler(_identityService.Object,_tokenService.Object,_logger.Object,_mapper);
        }
        [Fact]
        public async Task CreateUserCommandHandler_Successfully() {
            //Arange
            var command = new CreateUserCommand {
                Email = "test@gmail.com",
                UserName = "TestUser",
                Password = "Test123123"
            };
            var response = new Response<AuthenticationResponse> {
                Successful = true,
                Result = new AuthenticationResponse {
                    Email = command.Email,
                    Username = command.UserName,
                    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U",
                    RefreshToken = "B59DBA80-7AC4-42A9-0342-08DBA603F143"
                }
            };
            _identityService.Setup(x => x.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            _tokenService.Setup(x => x.GenerateUserTokenAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(response);
            //Act
            var result = await _commandHandler.Handle(command, new CancellationToken());
            //Assert
            _logger.VerifyLog(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(2));
            Assert.NotNull(result);
            Assert.True(result.Successful);
            Assert.Equal(result.Result.Email, command.Email);
        }
        [Fact]
        public async Task CreateUserCommandHandler_Unsuccessfully() {
            //Arange
            var command = new CreateUserCommand {
                Email = "test@gmail.com",
                UserName = "TestUser",
                Password = "Test123123"
            };
            var response = new Response<AuthenticationResponse> {
                Successful = false,
                Result = null
            };
            _identityService.Setup(x => x.CreateUserAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            //Act
            var result = await _commandHandler.Handle(command, new CancellationToken());
            //Assert
            _logger.VerifyLog(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(1));
            Assert.NotNull(result);
            Assert.False(result.Successful);
        }
    }
}
