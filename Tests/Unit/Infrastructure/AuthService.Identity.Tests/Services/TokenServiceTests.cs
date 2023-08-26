using AuthService.Application.Common.Interfaces.Repository;
using AuthService.Domain.Entities;
using AuthService.Identity.Models;
using AuthService.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace AuthService.Identity.Tests.Services {
    public class TokenServiceTests {
        private readonly Mock<UserManager<ApplicationUser>> _userManager;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly TokenService _tokenService;
        private readonly Mock<IRepository<RefreshToken>> _refreshTokenRepository;
        public TokenServiceTests() {
            _userManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            _jwtSettings = Options.Create(new JwtSettings {
                Secret = "123123123124152312349214215342657fxgbXFsF32RASFSADF4R23",
                TokenLifeTime = TimeSpan.FromMinutes(50)
            });
            _refreshTokenRepository = new Mock<IRepository<RefreshToken>>();
            _tokenService = new TokenService(_jwtSettings!,  _refreshTokenRepository.Object);
        }
        [Fact]
        public async Task GenerateTokenAsync_Successfully() {
            //Arange
            var user = new ApplicationUser { Id = "testId", UserName = "testUsername", Email = "test@gmail.com" };
            var refreshToken = new RefreshToken {
                JwtId = "cf0088d2-11b3-4530-b6f1-554fcdc0a774",
                UserId = "c6f65059-b887-406a-b5bc-df58545b4a4c",
                CreatedDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };
            _refreshTokenRepository.Setup(x => x.Add(refreshToken)).ReturnsAsync(true);

            //Act
            var result = await _tokenService.GenerateUserTokenAsync(user);

            //Asert
            Assert.True(result.Successful);
        }
        //[Fact]
        //public async Task GenerateUserTokenAsync_Not_Successfully() {
        //    //Arrange
        //    RefreshToken token = new RefreshToken();
        //    _refreshTokenRepository.Setup(x => () => x.Add(token)).ReturnsAsync(new Exception());
        //    //Act
        //    var result = async () => await _tokenService.GenerateUserTokenAsync(new ApplicationUser { Id = "testId", UserName = "testUsername" });
        //    var exception = await Assert.ThrowsAsync<Exception>(result);
        //    //Assert
        //    Assert.NotNull(exception);
        //}

        [Fact]
        public async Task GetClaimsPrincipalFromToken_Successfully() {
            //Arange
            var tokenValue = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiJjNmY2NTA1OS1iODg3LTQwNmEtYjViYy1kZjU4NTQ1YjRhNGMiLCJlbWFpbCI6Im9yd3d3ZUBnbWFpbC5jb20iLCJqdGkiOiIxZjIwM2FmYy0xMTBjLTRhMzItYjY4NS02MzMwNjNkNGRhMDciLCJuYmYiOjE2OTMwODc5MDMsImV4cCI6MTY5MzA5MDkwMywiaWF0IjoxNjkzMDg3OTAzfQ.DXJzy2lQZoyjSEzpwHPnAVpS17mDys-QsBXq3PSzd5U";

            //Act
            var claimsPrincipal = _tokenService.GetClaimsPrincipalFromToken(tokenValue);
            //Assert
            Assert.NotNull(claimsPrincipal);
        }
    }
}
