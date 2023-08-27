using AuthService.Application.Handlers.User.Queries.AuthenticateUser;
using AuthService.Application.Interfaces;
using AuthService.Common.Responses;
using AuthService.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AuthService.Application.Tests.Handlers.User.Queries.AuthenticateUser {
    public class AuthenticateUserQueryHandlerTests {
        readonly Mock<IIdentityService> _identityService;
        readonly Mock<ITokenService> _tokenService;
        readonly Mock<ILogger<AuthenticateUserQueryHandler>> _logger;
        readonly AuthenticateUserQueryHandler _queryHandler = null;

        public AuthenticateUserQueryHandlerTests() {
            _identityService = new Mock<IIdentityService>();
            _logger = new Mock<ILogger<AuthenticateUserQueryHandler>>();
            _tokenService = new Mock<ITokenService>();
            _queryHandler = new AuthenticateUserQueryHandler(_logger.Object, _identityService.Object, _tokenService.Object);
        }
        [Fact]
        public async Task AuthenciteUserQueryHandler_Successfully() {
            //Arange
            var query = new AuthenticateUserQuery {
                Email = "test@gmail.com",
                Password = "Test123123"
            };
            var testUser = new ApplicationUser {
                Email = query.Email,
                UserName = "TestUser"
            };
            var response = new Response<AuthenticationResponse>() {
            Successful = true,
            Result = new AuthenticationResponse {
                Email = testUser.Email,
                Username = testUser.UserName,
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U",
                RefreshToken = "B59DBA80-7AC4-42A9-0342-08DBA603F143"
            }
            };
            _identityService.Setup(x => x.FindByEmailAsync(query.Email)).ReturnsAsync(testUser);
            _identityService.Setup(x => x.CheckPasswordAsync(testUser, query.Password)).ReturnsAsync(true);
            _tokenService.Setup(x => x.GenerateUserTokenAsync(testUser)).ReturnsAsync(response);
            //Act
            var result = await _queryHandler.Handle(query, new CancellationToken());
            //Asert
            _logger.VerifyLog(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(2));
            Assert.NotNull(result);
            Assert.True(result.Successful);
            Assert.Equal(result.Result.Email, query.Email);
        }
        [Fact]
        public async Task AuthenciteUserQueryHandler_Unsuccessfully() {
            //Arange
            var query = new AuthenticateUserQuery {
                Email = "test@gmail.com",
                Password = "Test123123"
            };
            var testUser = new ApplicationUser {
                Email = query.Email,
                UserName = "TestUser"
            };
            var response = new Response<AuthenticationResponse>() {
                Successful = false,
                Result = new AuthenticationResponse {
                    Email = testUser.Email,
                    Username = testUser.UserName,
                    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U",
                    RefreshToken = "B59DBA80-7AC4-42A9-0342-08DBA603F143"
                }
            };
            _identityService.Setup(x => x.FindByEmailAsync(query.Email)).ReturnsAsync(testUser);
            _identityService.Setup(x => x.CheckPasswordAsync(testUser, query.Password)).ReturnsAsync(false);
            _tokenService.Setup(x => x.GenerateUserTokenAsync(testUser)).ReturnsAsync(response);
            //Act
            var result = await _queryHandler.Handle(query, new CancellationToken());
            //Asert
            _logger.VerifyLog(logger => logger.LogInformation(It.IsAny<string>()), Times.Exactly(1));
            Assert.Null(result);
            Assert.False(result.Successful);        }
    }
}
