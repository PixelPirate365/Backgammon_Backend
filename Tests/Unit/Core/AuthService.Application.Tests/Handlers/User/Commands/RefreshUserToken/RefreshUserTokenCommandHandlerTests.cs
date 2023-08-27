using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Application.Handlers.User.Commands.CreateUser;
using AuthService.Application.Handlers.User.Commands.RefreshUserToken;
using AuthService.Application.Interfaces;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Commands.RefreshUserToken {
    public class RefreshUserTokenCommandHandlerTests {
        readonly Mock<IIdentityService> _identityService;
        readonly Mock<ITokenService> _tokenService;
        readonly Mock<ILogger<RefreshUserTokenCommandHandler>> _logger;
        readonly Mock<IRepository<RefreshToken>> _refreshTokenRepository;
        readonly RefreshUserTokenCommandHandler _commandHandler;
        public RefreshUserTokenCommandHandlerTests()
        {
            _identityService = new Mock<IIdentityService>();
            _logger = new Mock<ILogger<RefreshUserTokenCommandHandler>>();
            _tokenService = new Mock<ITokenService>();
            _refreshTokenRepository = new Mock<IRepository<RefreshToken>>();
            _commandHandler = new(_logger.Object, _tokenService.Object, _refreshTokenRepository.Object, _identityService.Object);
        }
        [Fact]
        public async Task RefreshUserTokenCommand_Successfully() {
            var command = new RefreshUserTokenCommand() {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U",
                RefreshToken = "EE59142A-F61C-4818-0341-08DBA603F143"
            };
        }

    }
}
